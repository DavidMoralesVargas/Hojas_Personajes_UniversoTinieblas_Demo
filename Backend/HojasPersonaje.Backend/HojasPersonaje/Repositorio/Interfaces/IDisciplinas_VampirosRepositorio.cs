using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IDisciplinas_VampirosRepositorio
    {
        Task<ActionResponse<ICollection<Disciplina_Vampiro>>> ComboAllAsync(int idVampiro);
    }
}
