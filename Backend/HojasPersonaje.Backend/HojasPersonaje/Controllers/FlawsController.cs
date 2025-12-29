using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class FlawsController : GenericoController<Flaw>
    {
        public FlawsController(IGenericoRepositorio<Flaw> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
