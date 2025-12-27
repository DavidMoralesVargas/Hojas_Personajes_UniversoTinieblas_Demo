namespace HojasPersonaje.Entidades
{
    public class Nota_Adicional
    {
        public int Id { get; set; }
        public string? Nota { get; set; }

        public Usuario? Jugador { get; set; }
        public int JugadorId { get; set; }

        public Cronica? Cronica { get; set; }
        public int CronicaId { get; set; }

    }
}
