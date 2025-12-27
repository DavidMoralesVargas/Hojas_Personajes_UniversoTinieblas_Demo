namespace HojasPersonaje.Entidades
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string? Nombre_Disciplina { get; set; }


        public ICollection<Habilidades_Disciplina>? Habilidades_Disciplina { get; set; }
        public ICollection<Disciplina_Vampiro>? Disciplina_Vampiro { get; set; }
        public ICollection<Disciplina_Personaje>? Disciplina_Personajes { get; set; }
    }
}
