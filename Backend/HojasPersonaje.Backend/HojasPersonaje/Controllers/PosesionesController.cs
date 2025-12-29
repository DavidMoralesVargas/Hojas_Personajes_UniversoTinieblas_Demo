using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PosesionesController : GenericoController<Posesiones>
    {
        public PosesionesController(IGenericoRepositorio<Posesiones> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
