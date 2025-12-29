using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HabilidadesController : GenericoController<Habilidad>
    {
        public HabilidadesController(IGenericoRepositorio<Habilidad> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
