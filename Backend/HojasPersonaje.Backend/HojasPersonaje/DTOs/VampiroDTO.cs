using HojasPersonaje.Entidades;

namespace HojasPersonaje.DTOs
{
    public class VampiroDTO
    {
        public int VampiroId { get; set; }
        public string? NombreVampiro { get; set; }
        public Clan_Bane? ClanBane { get; set; }
        public List<Disciplina>? Disciplinas { get; set; }
    }
}
