namespace HojasPersonaje.Entidades
{
    public class Disciplina_Personaje
    {
        public int Id { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }

        public Disciplina? Disciplina { get; set; }
        public int DisciplinaId { get; set; }

    }
}
