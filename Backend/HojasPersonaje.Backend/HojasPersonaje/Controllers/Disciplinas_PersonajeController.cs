using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Disciplinas_PersonajeController : GenericoController<Disciplina_Personaje>
    {
        public Disciplinas_PersonajeController(IGenericoRepositorio<Disciplina_Personaje> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
