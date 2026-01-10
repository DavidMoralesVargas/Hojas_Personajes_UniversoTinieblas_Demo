using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HabilidadesDisciplinasController : GenericoController<Habilidades_Disciplina>
    {
        private readonly IUsuariosRepositorio _usuarioRepositorio;
        private readonly IHabilidades_DisciplinasRepositorio _disciplinasRepositorio;

        public HabilidadesDisciplinasController(IGenericoRepositorio<Habilidades_Disciplina> genericoRepositorio, IUsuariosRepositorio usuariosRepositorio, IHabilidades_DisciplinasRepositorio disciplinasRepositorio) : base(genericoRepositorio)
        {
            _usuarioRepositorio = usuariosRepositorio;
            _disciplinasRepositorio = disciplinasRepositorio;
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _disciplinasRepositorio.ObtenerPorIdAsync(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpPut]
        public override async Task<IActionResult> Actualizar([FromBody] Habilidades_Disciplina entidad)
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


        [HttpPost]
        public override async Task<IActionResult> Agregar([FromBody] Habilidades_Disciplina entidad)
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

        [HttpGet("disciplina/{id}")]
        public async Task<IActionResult> ObtenerPorDisciplina(int id)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _disciplinasRepositorio.ObtenerTodosPorIdAsync(id);
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
    }
}
