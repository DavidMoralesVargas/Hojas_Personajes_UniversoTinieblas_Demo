using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VampirosController : GenericoController<Vampiro>
    {
        public VampirosController(IGenericoRepositorio<Vampiro> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
