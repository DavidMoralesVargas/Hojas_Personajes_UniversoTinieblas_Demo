using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class DisciplinasRepositorio : GenericoRepositorio<Disciplina>, IDisciplinasRepositorio
    {
        private readonly IValidator<Disciplina> _validator;
        private readonly ClaseContexto _contexto;

        public DisciplinasRepositorio(IValidator<Disciplina> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }


        public override async Task<ActionResponse<IEnumerable<Disciplina>>> ObtenerTodosAsync()
        {
            try
            {
                return new ActionResponse<IEnumerable<Disciplina>>
                {
                    Exitoso = true,
                    Resultado = await _contexto.Disciplinas
                                              .Include(hd => hd.Habilidades_Disciplina)
                                              .ToListAsync(),
                    Mensaje = "Registros obtenidos con éxito"
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<IEnumerable<Disciplina>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<Disciplina>> AgregarAsync(DisciplinaDTO entidad)
        {
            try
            {
                var disciplina = new Disciplina
                {
                    Nombre_Disciplina = entidad.Nombre_Disciplina,
                    Habilidades_Disciplina = new List<Habilidades_Disciplina>()
                };

                foreach (var disciplina_habilidad in entidad.Habilidades!)
                {
                    disciplina.Habilidades_Disciplina!.Add(disciplina_habilidad);
                }

                _contexto.Disciplinas.Add(disciplina);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<Disciplina>
                {
                    Exitoso = true,
                    Resultado = disciplina,
                    Mensaje = "Registro creado con éxito"
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public override async Task<ActionResponse<Disciplina>> ActualizarAsync(Disciplina entidad)
        {
            try
            {
                var disciplina = await _contexto.Disciplinas
                                                .Include(hd => hd.Habilidades_Disciplina)
                                                .FirstOrDefaultAsync(x => x.Id == entidad.Id);
                if (disciplina == null)
                {
                    return new ActionResponse<Disciplina>
                    {
                        Exitoso = false,
                        Mensaje = "Registro no encontrado."
                    };
                }

                disciplina.Nombre_Disciplina = entidad.Nombre_Disciplina;

                _contexto.Update(disciplina);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<Disciplina>
                {
                    Exitoso = true,
                    Resultado = disciplina,
                    Mensaje = "Registro actualizado con éxito"
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<Disciplina>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public override async Task<ActionResponse<bool>> EliminarAsync(int id)
        {
            try
            {
                var disciplina = await _contexto.Disciplinas
                                .Include(hd => hd.Habilidades_Disciplina)
                                .FirstOrDefaultAsync(x => x.Id == id);
                if (disciplina == null)
                {
                    return new ActionResponse<bool>
                    {
                        Exitoso = false,
                        Mensaje = "Registro no encontrado."
                    };
                }

                _contexto.Habilidades_Disciplina.RemoveRange(disciplina.Habilidades_Disciplina!);

                _contexto.Disciplinas.Remove(disciplina);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<bool>
                {
                    Exitoso = true,
                    Resultado = true,
                    Mensaje = "Registro eliminado con éxito"
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
