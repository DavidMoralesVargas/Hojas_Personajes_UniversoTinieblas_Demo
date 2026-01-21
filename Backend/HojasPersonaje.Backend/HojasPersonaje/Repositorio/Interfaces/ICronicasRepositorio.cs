using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface ICronicasRepositorio
    {
        Task<ActionResponse<Cronica>> AgregarFullAsync(Cronica entidad, string DungeonMasterId);

        Task<ActionResponse<IEnumerable<Cronica>>> ObtenerTodosAsync();

        Task<ActionResponse<Cronica>> ActualizarFullAsync(Cronica entidad, string DungeonMasterId);

        Task<ActionResponse<bool>> CambiarFinalizacionAsync(int id, bool estado);

        Task<ActionResponse<Cronica>> ObtenerPorIdAsync(int id);
    }
}
