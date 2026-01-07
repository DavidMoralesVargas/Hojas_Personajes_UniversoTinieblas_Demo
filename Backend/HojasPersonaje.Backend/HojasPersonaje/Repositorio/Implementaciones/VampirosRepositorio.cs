using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class VampirosRepositorio : GenericoRepositorio<Vampiro>, IVampirosRepositorio
    {
        private readonly IValidator<Vampiro> _validator;
        private readonly ClaseContexto _contexto;

        public VampirosRepositorio(IValidator<Vampiro> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }



        //Verificar que un registro se encuentre en la base de datos por nombre
        public async Task<ActionResponse<bool>> verificarExistencia(string nombreVampiro)
        {
            var existe = await _contexto.Vampiros.FirstOrDefaultAsync(v => v.Nombre == nombreVampiro); //Filtra el nombre
            if (existe == null)  //Valida la existencia del registro
            {
                //Si es nulo, retorna false
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Resultado = false,
                    Mensaje = "El vampiro no existe."
                };
            }
            //Si se encontró el registro, retorna true
            return new ActionResponse<bool>
            {
                Exitoso = true,
                Resultado = true,
                Mensaje = "El vampiro existe."
            };
        }

        //Método para devolver un combo de los vampiros
        public async Task<ActionResponse<ICollection<Vampiro>>> ComboAsync()
        {
            try
            {
                var vampiros = await _contexto.Vampiros.ToListAsync();
                return new ActionResponse<ICollection<Vampiro>>
                {
                    Exitoso = true,
                    Resultado = vampiros
                };

            }
            catch (Exception ex)
            {
                return new ActionResponse<ICollection<Vampiro>>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al obtener los registros {ex.Message}"
                };
            }
        }

        public override async Task<ActionResponse<bool>> EliminarAsync(int id)
        {
            try
            {
                var vampiro = await _contexto.Vampiros
                                                      .Include(vd => vd.Disciplina_Vampiro!)
                                                      .Include(cb => cb.Clanes_Banes!)
                                                      .FirstOrDefaultAsync(x => x.Id == id);
                if (vampiro == null)
                {
                    return new ActionResponse<bool>
                    {
                        Exitoso = false,
                        Mensaje = "El vampiro ha eliminar no existe."
                    };
                }
                _contexto.Disciplinas_Vampiro.RemoveRange(vampiro.Disciplina_Vampiro!);
                _contexto.Clan_Banes.RemoveRange(vampiro.Clanes_Banes!);

                _contexto.Vampiros.Remove(vampiro);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<bool>
                {
                    Exitoso = true,
                    Mensaje = "Registro eliminado con éxito",
                    Resultado = true
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Mensaje = $"Ha ocurrido un error al eliminar el registro: {ex.Message}"
                };
            }
        }


        //Método para listar todos los vampiros, con los clan bane y sus disciplinas
        public override async Task<ActionResponse<IEnumerable<Vampiro>>> ObtenerTodosAsync()
        {
            try
            {
                var registros = await _contexto.Vampiros
                                               .Include(c => c.Clanes_Banes)
                                               .Include(dv => dv.Disciplina_Vampiro!)
                                               .ToListAsync();
                return new ActionResponse<IEnumerable<Vampiro>>
                {
                    Exitoso = true,
                    Resultado = registros
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<IEnumerable<Vampiro>>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al obtener los registros {ex.Message}"
                };
            }
        }

        public override async Task<ActionResponse<Vampiro>> ObtenerPorIdAsync(int id)
        {
            try
            {
                return new ActionResponse<Vampiro>
                {
                    Mensaje = "Registro encontrado con éxito",
                    Exitoso = true,
                    Resultado = await _contexto.Vampiros.Include(cb => cb.Clanes_Banes)
                                                        .Include(dv => dv.Disciplina_Vampiro)!
                                                        .ThenInclude(d => d.Disciplina)
                                                        .FirstOrDefaultAsync(x => x.Id == id)
                                                        
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<Vampiro>> AgregarAsync(VampiroDTO entidad)
        {
            try
            {
                var vampiro = new Vampiro
                {
                    Nombre = entidad.NombreVampiro,
                    Clanes_Banes = new List<Clan_Bane>(),
                    Disciplina_Vampiro = new List<Disciplina_Vampiro>()
                };

                if (entidad.ClanBane != null)
                {
                    vampiro.Clanes_Banes!.Add(new Clan_Bane
                    {
                        Bane = entidad.ClanBane.Bane,
                        Compulsion = entidad.ClanBane.Compulsion
                    });
                }

                foreach (var disciplina in entidad.Disciplinas!)
                {
                    var nuevadisciplina = await _contexto.Disciplinas.FirstOrDefaultAsync(d => d.Nombre_Disciplina == disciplina.Nombre_Disciplina);
                    if (nuevadisciplina != null)
                    {
                        vampiro.Disciplina_Vampiro!.Add(new Disciplina_Vampiro
                        {
                            DisciplinaId = nuevadisciplina.Id
                        });
                    }
                }

                _contexto.Vampiros.Add(vampiro);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<Vampiro>
                {
                    Exitoso = true,
                    Mensaje = "Registro agregado con éxito",
                    Resultado = vampiro
                };

            }
            catch (Exception ex)
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = false,
                    Mensaje = $"Ha ocurrido un error al agregar el registro: {ex.Message}"
                };
            }

        }

        public async Task<ActionResponse<Vampiro>> ActualizarAsync(VampiroDTO entidad)
        {
            try
            {
                var vampiro = await _contexto.Vampiros
                                       .Include(x => x.Clanes_Banes)
                                       .Include(dv => dv.Disciplina_Vampiro)!
                                       .ThenInclude(d => d.Disciplina)
                                       .FirstOrDefaultAsync(x => x.Id == entidad.VampiroId);
                if (vampiro == null)
                {
                    return new ActionResponse<Vampiro>
                    {
                        Exitoso = false,
                        Mensaje = "El vampiro no existe."
                    };
                }

                vampiro.Id = entidad.VampiroId;
                vampiro.Nombre = entidad.NombreVampiro;
                _contexto.Disciplinas_Vampiro.RemoveRange(vampiro.Disciplina_Vampiro!);
                _contexto.Clan_Banes.RemoveRange(vampiro.Clanes_Banes!);

                vampiro.Clanes_Banes = new List<Clan_Bane>();
                vampiro.Disciplina_Vampiro = new List<Disciplina_Vampiro>();

                vampiro.Clanes_Banes = new List<Clan_Bane>
                {
                    new Clan_Bane
                    {
                        Bane = entidad.ClanBane!.Bane,
                        Compulsion = entidad.ClanBane.Compulsion
                    }
                };

                foreach (var disciplina in entidad.Disciplinas!)
                {
                    var nuevadisciplina = await _contexto.Disciplinas.FirstOrDefaultAsync(d => d.Nombre_Disciplina == disciplina.Nombre_Disciplina);
                    if (nuevadisciplina != null)
                    {
                        vampiro.Disciplina_Vampiro!.Add(new Disciplina_Vampiro
                        {
                            DisciplinaId = nuevadisciplina.Id
                        });
                    }
                }

                _contexto.Update(vampiro);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<Vampiro>
                {
                    Exitoso = true,
                    Mensaje = "Registro actualizado con éxito",
                    Resultado = vampiro
                };

            }
            catch (Exception ex)
            {
                return new ActionResponse<Vampiro>
                {
                    Exitoso = false,
                    Mensaje = $"Ha ocurrido un error al agregar el registro: {ex.Message}"
                };
            }
        }
    }
}
