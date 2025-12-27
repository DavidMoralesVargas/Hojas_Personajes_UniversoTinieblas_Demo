namespace HojasPersonaje.Entidades
{
    public class Posesiones
    {
        public int Id { get; set; }
        public string? Posesion { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
