namespace HojasPersonaje.Entidades
{
    public class Hoja_Personaje
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Ambicion { get; set; }
        public int Generacion { get; set; }
        public string? Concepto { get; set; }
        public string? Sire { get; set; }
        public string? Desire { get; set; }
        public string? Titulo { get; set; }

        public Tipo_Depredador? Tipo_Depredador { get; set; }
        public int Tipo_DepredadorId { get; set; }

        public Cronica? Cronica { get; set; }
        public int CronicaId { get; set; }

        public Usuario? Jugador { get; set; }
        public string? JugadorId { get; set; }

        public Vampiro? Tipo_Vampiro { get; set; }
        public int Tipo_VampiroId { get; set; }


        public ICollection<Disciplina_Personaje>? Disciplina_Personajes { get; set; }
        public ICollection<Notas_Principales>? Notas_Principales { get; set; }
        public ICollection<Posesiones>? Posesiones { get; set; }
        public ICollection<Atributo_Hoja_Personaje>? Atributos_Hoja_Personaje { get; set; }
        public ICollection<Biografia>? Biografias { get; set; }
        public ICollection<Background>? Backgrounds { get; set; }
        public ICollection<Weapon>? Weapons { get; set; }
        public ICollection<Merito>? Meritos { get; set; }
        public ICollection<Habilidad>? Habilidades { get; set; }
        public ICollection<Experiencia_Hoja>? Experiencias_Hojas { get; set; }
        public ICollection<Conviccion_Piedras>? Convicciones_Piedras { get; set; }
        public ICollection<HabilidadesPersonaje>? HabilidadesPersonajes { get; set; }
    }
}
