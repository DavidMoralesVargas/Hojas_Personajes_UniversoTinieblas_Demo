namespace HojasPersonaje.Entidades
{
    public class Merito
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Nivel { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
