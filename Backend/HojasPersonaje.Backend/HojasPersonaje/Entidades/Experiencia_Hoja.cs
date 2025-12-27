
namespace HojasPersonaje.Entidades
{
    public class Experiencia_Hoja
    {
        public int Id { get; set; }
        public int Experiencia { get; set; }
        public int Experiencia_Gastada { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }
    }
}
