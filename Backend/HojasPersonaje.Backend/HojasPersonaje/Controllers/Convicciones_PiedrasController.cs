using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Convicciones_PiedrasController : GenericoController<Conviccion_Piedras>
    {
        public Convicciones_PiedrasController(IGenericoRepositorio<Conviccion_Piedras> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
