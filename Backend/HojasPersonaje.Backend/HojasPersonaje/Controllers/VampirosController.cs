using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VampirosController : GenericoController<Vampiro>
    {
        private readonly IVampirosRepositorio _vampirosRepositorio;

        public VampirosController(IGenericoRepositorio<Vampiro> genericoRepositorio, IVampirosRepositorio vampirosRepositorio) : base(genericoRepositorio)
        {
            _vampirosRepositorio = vampirosRepositorio;
        }

        //Método GET para verificar que un registro se encuentre en la base de datos
        [HttpGet("verificarExistencia/{nombreVampiro}")]
        public async Task<IActionResult> verificarExistencia(string nombreVampiro)
        {
            var resultado = await _vampirosRepositorio.verificarExistencia(nombreVampiro);

            return Ok(resultado);
        }
    }
}
