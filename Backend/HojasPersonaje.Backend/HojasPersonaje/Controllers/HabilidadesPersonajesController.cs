using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HabilidadesPersonajesController : GenericoController<HabilidadesPersonaje>
    {
        public HabilidadesPersonajesController(IGenericoRepositorio<HabilidadesPersonaje> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
