using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BiografiasController : GenericoController<Biografia>
    {
        public BiografiasController(IGenericoRepositorio<Biografia> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
