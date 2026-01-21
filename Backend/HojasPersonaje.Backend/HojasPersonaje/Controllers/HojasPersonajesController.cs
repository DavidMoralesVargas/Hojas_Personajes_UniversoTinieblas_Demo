using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HojasPersonajesController : GenericoController<Hoja_Personaje>
    {
        private readonly IHojas_PersonajeRepositorio _hojasPersonajeRepositorio;
        public HojasPersonajesController(IGenericoRepositorio<Hoja_Personaje> genericoRepositorio, IHojas_PersonajeRepositorio hojas_PersonajeRepositorio) : base(genericoRepositorio)
        {
            _hojasPersonajeRepositorio = hojas_PersonajeRepositorio;
        }

        [HttpGet("buscarPorCronicaId")]
        public async Task<IActionResult> BuscarPorCronicaIdAsync([FromQuery] int cronicaId, [FromQuery] string idUsuario)
        {
            var respuesta = await _hojasPersonajeRepositorio.BuscarPorCronicaIdAsync(cronicaId, idUsuario);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
