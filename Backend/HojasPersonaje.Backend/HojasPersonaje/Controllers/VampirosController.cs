using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
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
    public class VampirosController : GenericoController<Vampiro>
    {
        private readonly IVampirosRepositorio _vampirosRepositorio;
        private readonly IUsuariosRepositorio _usuarioRepositorio;

        public VampirosController(IGenericoRepositorio<Vampiro> genericoRepositorio, IVampirosRepositorio vampirosRepositorio, IUsuariosRepositorio usuariosRepositorio) : base(genericoRepositorio)
        {
            _vampirosRepositorio = vampirosRepositorio;
            _usuarioRepositorio = usuariosRepositorio;
        }

        //Método GET para verificar que un registro se encuentre en la base de datos
        [HttpGet("verificarExistencia/{nombreVampiro}")]
        [AllowAnonymous]
        public async Task<IActionResult> verificarExistencia(string nombreVampiro)
        {
            var resultado = await _vampirosRepositorio.verificarExistencia(nombreVampiro);

            return Ok(resultado);
        }

        [HttpGet("combo")]
        [AllowAnonymous]
        public async Task<IActionResult> Combo()
        {
            var resultado = await _vampirosRepositorio.ComboAsync();
            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            return BadRequest(resultado);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _vampirosRepositorio.ObtenerPorIdAsync(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpGet]
        public override async Task<IActionResult> ObtenerTodos()
        {
            //Extraer email de los claims y verificar el rol del usuario
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }
            var resultado = await _vampirosRepositorio.ObtenerTodosAsync(); //Obtener los registros
            if (resultado.Exitoso) //Verificar proceso exitoso
            {
                return Ok(resultado);
            }
            return BadRequest(resultado);
        }

        [HttpPost("vampiroAll")]
        public async Task<IActionResult> Agregar([FromBody] VampiroDTO entidad)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _vampirosRepositorio.AgregarAsync(entidad);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpPut("vampiroAll")]
        public async Task<IActionResult> Actualizar([FromBody] VampiroDTO entidad)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarioRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var resultado = await _vampirosRepositorio.ActualizarAsync(entidad); 
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

            var resultado = await _vampirosRepositorio.EliminarAsync(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }
    }
}
