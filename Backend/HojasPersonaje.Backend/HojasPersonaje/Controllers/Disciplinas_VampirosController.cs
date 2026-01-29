using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Disciplinas_VampirosController : GenericoController<Disciplina_Vampiro>
    {
        private readonly IDisciplinas_VampirosRepositorio _disciplinas_VampirosRepositorio;
        public Disciplinas_VampirosController(IGenericoRepositorio<Disciplina_Vampiro> genericoRepositorio, IDisciplinas_VampirosRepositorio disciplinas_VampirosRepositorio) : base(genericoRepositorio)
        {
            _disciplinas_VampirosRepositorio = disciplinas_VampirosRepositorio;
        }

        [HttpGet("comboAll")]
        public async Task<IActionResult> ComboComboAllAsyncAsync([FromQuery] int idVampiro)
        {
            var respuesta = await _disciplinas_VampirosRepositorio.ComboAllAsync(idVampiro);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
