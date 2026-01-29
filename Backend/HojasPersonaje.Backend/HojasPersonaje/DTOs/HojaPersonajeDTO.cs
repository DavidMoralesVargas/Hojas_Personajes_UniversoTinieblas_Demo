using HojasPersonaje.Entidades;

namespace HojasPersonaje.DTOs
{
    public class HojaPersonajeDTO
    {
        public Hoja_Personaje? HojaPersonaje { get; set; }
        public Experiencia_Hoja? Experiencia { get; set; }
        public Conviccion_Piedras? ConviccionPiedras { get; set; }
        public Habilidad? Habilidad { get; set; }
        public Flaw? Flaw { get; set; }
        public Weapon? Weapon { get; set; }
        public Merito? Merito { get; set; }
        public Background? Background { get; set; }
        public Biografia? Biografia { get; set; }
        public Atributo_Hoja_Personaje? Atributo_Hoja_Personaje { get; set; }
        public Posesiones? Posesiones { get; set; }
        public Notas_Principales? Notas_Principales { get; set; }
    }
}
