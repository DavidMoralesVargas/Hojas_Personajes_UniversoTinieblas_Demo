using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeaponsController : GenericoController<Weapon>
    {
        public WeaponsController(IGenericoRepositorio<Weapon> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
