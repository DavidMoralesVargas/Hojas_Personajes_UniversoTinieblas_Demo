using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Tipos_DepredadorController : GenericoController<Tipo_Depredador>
    {
        private readonly ITipos_DepredadorRepositorio _tipos_DepredadorRepositorio;

        public Tipos_DepredadorController(IGenericoRepositorio<Tipo_Depredador> genericoRepositorio, ITipos_DepredadorRepositorio tipos_DepredadorRepositorio) : base(genericoRepositorio)
        {
            _tipos_DepredadorRepositorio = tipos_DepredadorRepositorio;
        }

        [HttpGet("combo")]
        public async Task<IActionResult> ComboAsync()
        {
            var respuesta = await _tipos_DepredadorRepositorio.ComboAsync();
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
