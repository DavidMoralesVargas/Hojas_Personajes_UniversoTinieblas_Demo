using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Habilidades_DisciplinasController : GenericoController<Habilidades_Disciplina>
    {
        public Habilidades_DisciplinasController(IGenericoRepositorio<Habilidades_Disciplina> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
