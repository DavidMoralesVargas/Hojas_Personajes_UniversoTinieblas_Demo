using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Especialidades_HabilidadesController : GenericoController<Especialidad_Habilidad>
    {
        public Especialidades_HabilidadesController(IGenericoRepositorio<Especialidad_Habilidad> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
