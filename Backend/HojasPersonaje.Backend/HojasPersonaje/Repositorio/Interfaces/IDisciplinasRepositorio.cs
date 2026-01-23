using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IDisciplinasRepositorio
    {
        Task<ActionResponse<IEnumerable<Disciplina>>> ObtenerTodosAsync();
        Task<ActionResponse<Disciplina>> AgregarAsync(DisciplinaDTO entidad);
        Task<ActionResponse<Disciplina>> ActualizarAsync(Disciplina entidad);
        Task<ActionResponse<bool>> EliminarAsync(int id);
        Task<ActionResponse<Disciplina>> ObtenerPorIdAsync(int id);
        Task<ActionResponse<IEnumerable<Disciplina>>> ComboAsync();
    }
}
