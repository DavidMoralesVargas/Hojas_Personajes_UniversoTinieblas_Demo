using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Notas_PrincipalesController : GenericoController<Notas_Principales>
    {
        public Notas_PrincipalesController(IGenericoRepositorio<Notas_Principales> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
