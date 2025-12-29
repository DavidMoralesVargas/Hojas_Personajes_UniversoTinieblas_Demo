using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Disciplinas_VampirosController : GenericoController<Disciplina_Vampiro>
    {
        public Disciplinas_VampirosController(IGenericoRepositorio<Disciplina_Vampiro> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
