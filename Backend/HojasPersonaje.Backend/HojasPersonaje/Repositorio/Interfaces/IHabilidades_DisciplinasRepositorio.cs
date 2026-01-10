using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IHabilidades_DisciplinasRepositorio
    {
        Task<ActionResponse<IEnumerable<Habilidades_Disciplina>>> ObtenerTodosPorIdAsync(int id);
        Task<ActionResponse<Habilidades_Disciplina>> AgregarAsync(Habilidades_Disciplina entidad);
        Task<ActionResponse<Habilidades_Disciplina>> ActualizarAsync(Habilidades_Disciplina entidad);
        Task<ActionResponse<bool>> EliminarAsync(int id);
        Task<ActionResponse<Habilidades_Disciplina>> ObtenerPorIdAsync(int id);
    }
}
