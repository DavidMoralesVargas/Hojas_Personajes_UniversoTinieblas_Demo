namespace HojasPersonaje.Entidades
{
    public class Vampiro
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }


        public ICollection<Clan_Bane>? Clanes_Banes { get; set; }
        public ICollection<Hoja_Personaje>? Hojas_Personaje { get; set; }
        public ICollection<Disciplina_Vampiro>? Disciplina_Vampiro { get; set; }
    }
}
