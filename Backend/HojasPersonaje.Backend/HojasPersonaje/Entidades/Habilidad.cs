namespace HojasPersonaje.Entidades
{
    public class Habilidad
    {
        public int Id { get; set; }
        public int Atletismo { get; set; }
        public int Pelea { get; set; }
        public int Crafteo { get; set; }
        public int Conduccion { get; set; }
        public int Armas_Fuego { get; set; }
        public int Latrocinio { get; set; }
        public int Melee { get; set; }
        public int Sigilo { get; set; }
        public int Supervivencia { get; set; }
        public int Animal_Ken { get; set; }
        public int Etiqueta { get; set; }
        public int Insight { get; set; }
        public int Intimidacion { get; set; }
        public int Liderazgo { get; set; }
        public int Actuacion { get; set; }
        public int Persuasion { get; set; } 
        public int Astucia { get; set; }
        public int Subterfugio { get; set; }
        public int Academicismo { get; set; }
        public int Consciencia { get; set; }
        public int Finanzas { get; set; }
        public int Investigacion{ get; set; }
        public int Medicina { get; set; }
        public int Ocultismo { get; set; }
        public int Politica { get; set; }
        public int Ciencia { get; set; }
        public int Tecnologia { get; set; }

        public Hoja_Personaje? Hoja_Personaje { get; set; }
        public int Hoja_PersonajeId { get; set; }

        public ICollection<Especialidad_Habilidad>? Especialidades_Habilidad { get; set; }

    }
}
