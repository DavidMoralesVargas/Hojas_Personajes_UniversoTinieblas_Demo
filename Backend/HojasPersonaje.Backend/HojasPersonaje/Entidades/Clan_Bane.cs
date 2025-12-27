namespace HojasPersonaje.Entidades
{
    public class Clan_Bane
    {
        public int Id { get; set; }
        public string? Bane { get; set; }
        public string? Compulsion { get; set; }
        public Vampiro? Vampiro { get; set; }
        public int VampiroId { get; set; }
    }
}
