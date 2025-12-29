using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Principios_CronicasController : GenericoController<Principio_Cronica>
    {
        public Principios_CronicasController(IGenericoRepositorio<Principio_Cronica> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
