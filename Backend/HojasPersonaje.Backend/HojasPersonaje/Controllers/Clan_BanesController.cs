using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Clan_BanesController : GenericoController<Clan_Bane>
    {
        public Clan_BanesController(IGenericoRepositorio<Clan_Bane> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
