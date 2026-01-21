using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using HojasPersonaje.Signal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class CronicasRepositorio : GenericoRepositorio<Cronica>, ICronicasRepositorio
    {
        private readonly IValidator<Cronica> _validator;
        private readonly ClaseContexto _contexto;
        private readonly IHubContext<ClaseHub> _hubContext;
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public CronicasRepositorio(IValidator<Cronica> validator, ClaseContexto contexto, IHubContext<ClaseHub> hubContext, IUsuariosRepositorio usuariosRepositorio) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
            _hubContext = hubContext;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<ActionResponse<bool>> CambiarFinalizacionAsync(int id, bool estado)
        {
            try
            {
                var entidad = await _contexto.Cronicas.FindAsync(id);
                if (entidad == null)
                {
                    return new ActionResponse<bool>
                    {
                        Exitoso = false,
                        Mensaje = "Cronica no encontrada."
                    };
                }

                entidad.Finalizado = estado;

                _contexto.Cronicas.Update(entidad);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<bool>
                {
                    Exitoso = true,
                    Mensaje = "Estado de finalización actualizado exitosamente.",
                    Resultado = true
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<ActionResponse<Cronica>> AgregarFullAsync(Cronica entidad, string DungeonMaster)
        {
            entidad.Finalizado = false;
            entidad.Fecha_Cronica = DateTime.Now;

            try
            {
                var usuario = await _usuariosRepositorio.GetUserAsync(DungeonMaster);
                if (usuario == null)
                {
                    return new ActionResponse<Cronica>
                    {
                        Exitoso = false
                    };
                }
                entidad.Dungeon_MasterId = usuario.Id;

                _contexto.Cronicas.Add(entidad);
                await _contexto.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("NuevaCronicaCreada", entidad.Id);
                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Mensaje = "Cronica agregada exitosamente.",
                    Resultado = entidad
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public override async Task<ActionResponse<IEnumerable<Cronica>>> ObtenerTodosAsync()
        {
            try
            {
                return new ActionResponse<IEnumerable<Cronica>>
                {
                    Exitoso = true,
                    Mensaje = "Cronicas obtenidas exitosamente.",
                    Resultado = await _contexto.Cronicas.Include(x => x.Dungeon_Master).ToListAsync()
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<IEnumerable<Cronica>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }


        public async Task<ActionResponse<Cronica>> ActualizarFullAsync(Cronica entidad, string DungeonMaster)
        {
            entidad.Fecha_Cronica = DateTime.Now;

            try
            {
                var usuario = await _usuariosRepositorio.GetUserAsync(DungeonMaster);
                if (usuario == null)
                {
                    return new ActionResponse<Cronica>
                    {
                        Exitoso = false
                    };
                }
                entidad.Dungeon_MasterId = usuario.Id;

                _contexto.Update(entidad);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Mensaje = "Cronica actualizada exitosamente.",
                    Resultado = entidad
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

        public override async Task<ActionResponse<Cronica>> ObtenerPorIdAsync(int id)
        {
            try
            {
                return new ActionResponse<Cronica>
                {
                    Exitoso = true,
                    Mensaje = "Cronica obtenida exitosamente.",
                    Resultado = await _contexto.Cronicas.Include(x => x.Dungeon_Master).FirstOrDefaultAsync(x => x.Id == id)
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<Cronica>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }

    }
}
