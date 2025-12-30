namespace HojasPersonaje.Entidades
{
    public class Lista_Amigos
    {
        public int Id { get; set; }

        public Usuario? Usuario1 { get; set; }
        public string? Usuario1Id { get; set; }

        public Usuario? Usuario2 { get; set; }
        public string? Usuario2Id { get; set; }

        public DateTime Fecha_Amigos { get; set; }
    }
}
