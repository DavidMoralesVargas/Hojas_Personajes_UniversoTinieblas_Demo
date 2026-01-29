using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface ITipos_DepredadorRepositorio
    {
        Task<ActionResponse<ICollection<Tipo_Depredador>>> ComboAsync();
    }
}
