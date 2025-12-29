using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Hojas_PersonajesController : GenericoController<Hoja_Personaje>
    {
        public Hojas_PersonajesController(IGenericoRepositorio<Hoja_Personaje> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
