namespace HojasPersonaje.Entidades
{
    public class HabilidadesPersonaje
    {
        public int Id { get; set; }
        
        public Hoja_Personaje? Personaje { get; set; }
        public int PersonajeId { get; set; }

        public Habilidades_Disciplina? Disciplina { get; set; }
        public int DisciplinaId { get; set; }
    }
}
