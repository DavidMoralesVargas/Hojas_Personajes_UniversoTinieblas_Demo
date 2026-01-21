namespace HojasPersonaje.Entidades
{
    public class Cronica
    {
        public int Id { get; set; }
        public string? Nombre_Cronica { get; set; }
        public string? Pais_Cronica { get; set; }
        public DateTime Fecha_Cronica { get; set; }
        public bool Finalizado { get; set; }

        public Usuario? Dungeon_Master { get; set; }
        public string? Dungeon_MasterId { get; set; }

        public ICollection<Principio_Cronica>? Principios_Cronica { get; set; }
        public ICollection<Nota_Adicional>? Notas_Adicionales { get; set; }
        public ICollection<Hoja_Personaje>? Vampiros { get; set; }

    }
}
