using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DisciplinasController : GenericoController<Disciplina>
    {
        public DisciplinasController(IGenericoRepositorio<Disciplina> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
