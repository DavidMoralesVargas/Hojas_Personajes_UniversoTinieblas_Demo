using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class GenericoRepositorio<T> : IGenericoRepositorio<T> where T : class
    {
        private readonly IValidator<T> _validator;
        private readonly ClaseContexto _contexto;
        private readonly DbSet<T> _entidad;

        public GenericoRepositorio(IValidator<T> validator, ClaseContexto contexto)
        {
            _validator = validator;
            _contexto = contexto;
            _entidad = contexto.Set<T>();
        }

        public virtual async Task<ActionResponse<T>> ActualizarAsync(T entidad)
        {
            try
            {
                _entidad.Update(entidad);
                await _contexto.SaveChangesAsync();

                return new ActionResponse<T>
                {
                    Exitoso = true,
                    Mensaje = $"Registro actualizado correctamente",
                    Resultado = entidad
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<T>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al eliminar {ex.Message}"
                };
            }
        }

        public virtual async Task<ActionResponse<T>> AgregarAsync(T entidad)
        {
            var validationResult = await _validator.ValidateAsync(entidad);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            try
            {
                _entidad.Add(entidad);
                await _contexto.SaveChangesAsync();

                return new ActionResponse<T>
                {
                    Exitoso = true,
                    Mensaje = $"Registro guardado correctamente",
                    Resultado = entidad
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<T>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al crear el registro. {ex.Message}"
                };
            }
        }

        public virtual async Task<ActionResponse<bool>> EliminarAsync(int id)
        {
            try
            {
                var entidad = await _entidad.FindAsync(id);
                if (entidad == null)
                {
                    return new ActionResponse<bool>
                    {
                        Exitoso = false,
                        Mensaje = "Entidad no encontrada"
                    };
                }

                _entidad.Remove(entidad);
                await _contexto.SaveChangesAsync();
                return new ActionResponse<bool>
                {
                    Exitoso = true,
                    Mensaje = "Entidad no encontrada",
                    Resultado = true
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al eliminar {ex.Message}"
                };
            }
        }

        public virtual async Task<ActionResponse<T>> ObtenerPorIdAsync(int id)
        {
            try
            {
                var entidad = await _entidad.FindAsync(id);
                if (entidad == null)
                {
                    return new ActionResponse<T>
                    {
                        Exitoso = false,
                        Mensaje = "Entidad no encontrada"
                    };
                }

                return new ActionResponse<T>
                {
                    Exitoso = true,
                    Mensaje = "Entidad encontrada con éxito",
                    Resultado = entidad
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<T>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al eliminar {ex.Message}"
                };
            }
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> ObtenerTodosAsync()
        {
            try
            {
                var registros = await _entidad.ToListAsync();
                return new ActionResponse<IEnumerable<T>>
                {
                    Exitoso = true,
                    Mensaje = "Registros obtenidos con éxito",
                    Resultado = registros
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<IEnumerable<T>>
                {
                    Exitoso = false,
                    Mensaje = $"ha ocurrido un error al eliminar {ex.Message}"
                };
            }
        }
    }
}
