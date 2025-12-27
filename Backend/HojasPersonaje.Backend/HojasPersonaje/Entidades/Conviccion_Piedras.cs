namespace HojasPersonaje.Entidades
{
    public class Conviccion_Piedras
    {
        public int Id { get; set; }
        public string? Convicciones { get; set; }
        public string? Piedras_Token { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
