namespace HojasPersonaje.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre_Usuario { get; set; }
        public Tipo_Usuario Tipo_Usuario { get; set; }


        public ICollection<Cronica>? Cronicas { get; set; }
        public ICollection<Nota_Adicional>? Notas_Adicionales { get; set; }
        public ICollection<Hoja_Personaje>? Hojas_Personaje { get; set; }
    }
}
