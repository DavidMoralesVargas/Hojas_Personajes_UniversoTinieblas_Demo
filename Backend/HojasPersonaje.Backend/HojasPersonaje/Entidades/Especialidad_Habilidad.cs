namespace HojasPersonaje.Entidades
{
    public class Especialidad_Habilidad
    {
        public int Id { get; set; }
        public string? Especialidad { get; set; }

        public Habilidad? Habilidad { get; set; }
        public int HabilidadId { get; set; }
    }
}
