namespace HojasPersonaje.Entidades
{
    public class Disciplina_Vampiro
    {
        public int Id { get; set; }

        public Vampiro? Vampiro { get; set; }
        public int VampiroId { get; set; }

        public Disciplina? Disciplina { get; set; }
        public int DisciplinaId { get; set; }

    }
}
