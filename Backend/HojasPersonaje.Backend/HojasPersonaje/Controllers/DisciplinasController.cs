using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DisciplinasController : GenericoController<Disciplina>
    {
        private readonly IDisciplinasRepositorio _disciplinasRepositorio;
        private readonly IUsuariosRepositorio _usuarioRepositorio;

        public DisciplinasController(IGenericoRepositorio<Disciplina> genericoRepositorio, IDisciplinasRepositorio disciplinas, IUsuariosRepositorio usuariosRepositorio) : base(genericoRepositorio)
        {
            _disciplinasRepositorio = disciplinas;
            _usuarioRepositorio = usuariosRepositorio;
        }


        [HttpGet("combo")]
        [AllowAnonymous]
        public async Task<IActionResult> Combo()
        {
            var resultado = await _disciplinasRepositorio.ComboAsync();
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpPost("all")]
        public async Task<IActionResult> Agregar([FromBody] DisciplinaDTO entidad)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _disciplinasRepositorio.AgregarAsync(entidad);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Eliminar(int id)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _disciplinasRepositorio.EliminarAsync(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpPut]
        public override async Task<IActionResult> Actualizar([FromBody] Disciplina entidad)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _disciplinasRepositorio.ActualizarAsync(entidad);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpGet]
        public override async Task<IActionResult> ObtenerTodos()
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _disciplinasRepositorio.ObtenerTodosAsync();
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

    }
}
