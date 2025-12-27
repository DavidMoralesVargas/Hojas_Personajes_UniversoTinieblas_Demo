namespace HojasPersonaje.Entidades
{
    public class Notas_Principales
    {
        public int Id { get; set; }
        public string? Nota { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
