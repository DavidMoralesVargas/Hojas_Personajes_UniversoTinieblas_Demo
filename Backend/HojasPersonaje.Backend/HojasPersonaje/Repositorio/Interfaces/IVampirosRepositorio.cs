using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IVampirosRepositorio
    {
        Task<ActionResponse<bool>> verificarExistencia(string nombreVampiro);
        Task<ActionResponse<ICollection<Vampiro>>> ComboAsync();
        Task<ActionResponse<IEnumerable<Vampiro>>> ObtenerTodosAsync();
        Task<ActionResponse<Vampiro>> AgregarAsync(Vampiro entidad);
        Task<ActionResponse<Vampiro>> AgregarAsync(VampiroDTO entidad);
        Task<ActionResponse<Vampiro>> ActualizarAsync(VampiroDTO entidad);
        Task<ActionResponse<Vampiro>> ActualizarAsync(Vampiro entidad);
        Task<ActionResponse<bool>> EliminarAsync(int id);
        Task<ActionResponse<Vampiro>> ObtenerPorIdAsync(int id);

    }
}
