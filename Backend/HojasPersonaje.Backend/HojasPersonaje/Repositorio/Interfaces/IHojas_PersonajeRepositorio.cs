using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IHojas_PersonajeRepositorio
    {
        Task<ActionResponse<Hoja_Personaje>> BuscarPorCronicaIdAsync(int cronicaId, string idUsuario);
    }
}
