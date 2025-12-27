namespace HojasPersonaje.Entidades
{
    public class Tipo_Depredador
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public ICollection<Hoja_Personaje>? Hojas_Personaje { get; set; }
    }
}
