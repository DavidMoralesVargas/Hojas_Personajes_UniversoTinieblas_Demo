using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IVampirosRepositorio
    {
        Task<ActionResponse<bool>> verificarExistencia(string nombreVampiro);
    }
}
