namespace HojasPersonaje.Entidades
{
    public class Biografia
    {
        public int Id { get; set; }
        public int Edad_Real { get; set; }
        public int Edad_Aparente { get; set; }
        public DateOnly Fecha_Nacimiento { get; set; }
        public DateOnly Fecha_Muerte { get; set; }
        public string? Apariencia { get; set; }
        public string? Rasgos_Distintivos { get; set; }
        public string? Historia { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
