using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IGenericoRepositorio<T> where T : class
    {
        Task<ActionResponse<T>> AgregarAsync(T entidad);

        Task<ActionResponse<T>> ObtenerPorIdAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> ObtenerTodosAsync();

        Task<ActionResponse<T>> ActualizarAsync(T entidad);

        Task<ActionResponse<bool>> EliminarAsync(int id);
    }
}
