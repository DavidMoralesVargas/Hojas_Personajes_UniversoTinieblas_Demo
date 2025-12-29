using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    public class GenericoController<T> : ControllerBase where T : class
    {
        private readonly IGenericoRepositorio<T> _genericoRepositorio;

        public GenericoController(IGenericoRepositorio<T> genericoRepositorio)
        {
            _genericoRepositorio = genericoRepositorio;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Agregar([FromBody] T entidad)
        {
            var resultado = await _genericoRepositorio.AgregarAsync(entidad);
            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            return BadRequest(resultado);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _genericoRepositorio.ObtenerPorIdAsync(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            return NotFound(resultado);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _genericoRepositorio.EliminarAsync(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            return BadRequest(resultado);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Actualizar([FromBody] T entidad)
        {
            var resultado = await _genericoRepositorio.ActualizarAsync(entidad);
            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            return BadRequest(resultado);
        }

        [HttpGet]
        public virtual async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _genericoRepositorio.ObtenerTodosAsync();
            if (resultado.Exitoso)
            {
                return Ok(resultado);
            }
            return NotFound(resultado);
        }
    }
}
