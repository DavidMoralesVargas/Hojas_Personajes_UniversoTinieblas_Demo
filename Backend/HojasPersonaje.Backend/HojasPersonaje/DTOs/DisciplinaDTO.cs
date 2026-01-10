using HojasPersonaje.Entidades;

namespace HojasPersonaje.DTOs
{
    public class DisciplinaDTO
    {
        public int DisciplinaId { get; set; }
        public string? Nombre_Disciplina { get; set; }
        public List<Habilidades_Disciplina>? Habilidades { get; set; }
    }
}
