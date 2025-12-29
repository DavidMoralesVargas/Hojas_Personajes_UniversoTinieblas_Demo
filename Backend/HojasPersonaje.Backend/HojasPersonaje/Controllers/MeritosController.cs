using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MeritosController : GenericoController<Merito>
    {
        public MeritosController(IGenericoRepositorio<Merito> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
