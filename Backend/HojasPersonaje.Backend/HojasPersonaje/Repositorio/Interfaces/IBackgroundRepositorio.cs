using HojasPersonaje.Entidades;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IBackgroundRepositorio
    {
        Task<Background> AgregarBackgroundAsync(Background background);
    }
}
