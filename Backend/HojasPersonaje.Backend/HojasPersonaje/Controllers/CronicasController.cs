using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CronicasController : GenericoController<Cronica>
    {
        public CronicasController(IGenericoRepositorio<Cronica> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
