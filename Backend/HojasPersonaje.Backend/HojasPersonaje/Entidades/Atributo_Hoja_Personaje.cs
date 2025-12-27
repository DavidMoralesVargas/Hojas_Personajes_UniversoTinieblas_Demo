namespace HojasPersonaje.Entidades
{
    public class Atributo_Hoja_Personaje
    {
        public int Id { get; set; }
        public int Fuerza { get; set; }
        public int Destreza { get; set; } 
        public int Resistencia { get; set; }
        public int Carisma { get; set; }
        public int Manipulacion { get; set; }
        public int Compostura { get; set; }
        public int Inteligencia { get; set; }
        public int Astucia { get; set; }
        public int Resolucion { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
