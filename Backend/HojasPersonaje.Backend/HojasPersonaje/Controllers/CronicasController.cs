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
    public class CronicasController : GenericoController<Cronica>
    {
        private readonly ICronicasRepositorio _cronicasRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public CronicasController(IGenericoRepositorio<Cronica> genericoRepositorio, ICronicasRepositorio cronicasRepositorio, IUsuariosRepositorio usuariosRepositorio) : base(genericoRepositorio)
        {
            _cronicasRepositorio = cronicasRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public override async Task<IActionResult> ObtenerPorId(int id)
        {
            var respuesta = await _cronicasRepositorio.ObtenerPorIdAsync(id);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }


        [HttpGet]
        [AllowAnonymous]
        public override async Task<IActionResult> ObtenerTodos()
        {
            var respuesta = await _cronicasRepositorio.ObtenerTodosAsync();
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }




        [HttpPost]
        public override async Task<IActionResult> Agregar([FromBody] Cronica entidad)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuariosRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var respuesta = await _cronicasRepositorio.AgregarFullAsync(entidad, usuario!);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }


        [HttpPut("cambiarFinalizacion")]
        public async Task<IActionResult> CambiarFinalizacion(CronicaDTO cronicaDTO)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuariosRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var respuesta = await _cronicasRepositorio.CambiarFinalizacionAsync(cronicaDTO.Id, cronicaDTO.Finalizado);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }


        [HttpPut]
        public override async Task<IActionResult> Actualizar([FromBody] Cronica entidad)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuariosRepositorio.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var respuesta = await _cronicasRepositorio.ActualizarFullAsync(entidad, usuario!);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
