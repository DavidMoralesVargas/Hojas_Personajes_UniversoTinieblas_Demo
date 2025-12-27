namespace HojasPersonaje.Entidades
{
    public class Habilidades_Disciplina
    {
        public int Id { get; set; }
        public string? Nombre_Habilidad { get; set; }
        public int Nivel { get; set; }

        public Disciplina? Disciplina { get; set; }
        public int DisciplinaId { get; set; }

        public ICollection<HabilidadesPersonaje>? HabilidadesPersonajes { get; set; }
    }
}
