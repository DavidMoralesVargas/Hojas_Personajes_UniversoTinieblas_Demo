using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepositorio _usuarios;
        private readonly IConfiguration _configuration;
        public UsuariosController(IUsuariosRepositorio usuarios, IConfiguration configuration)
        {
            _usuarios = usuarios;
            _configuration = configuration;
        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDTO usuario)
        {
            Usuario modelo = usuario;
            modelo.UserName = $"{modelo.Nombre_Usuario!.ToLower()}@yopmail.com";
            modelo.Email = $"{modelo.Nombre_Usuario!.ToLower()}@yopmail.com";
            var resultado = await _usuarios.AddUserAsync(modelo, usuario.Contraseña!);
            if (resultado.Succeeded)
            {
                await _usuarios.AddUserToRoleAsync(modelo, modelo.Tipo_Usuario.ToString());
                return Ok(BuildToken(modelo));
            }
            return BadRequest(resultado.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO modelo)
        {
            modelo.Email = $"{modelo.Email!.ToLower()}@yopmail.com";
            var resultado = await _usuarios.LoginAsync(modelo);
            if (resultado.Succeeded)
            {
                var user = await _usuarios.GetUserAsync(modelo.Email!);
                return Ok(BuildToken(user));
            }
            return BadRequest("Email o contraseña incorrectos.");
        }

        private TokenDTO BuildToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Email!),
                new(ClaimTypes.Role, usuario.Tipo_Usuario.ToString()),
                new("Nombre_Usuario", usuario.Nombre_Usuario ?? ""),
                new("sub", usuario.Email!)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration, //Prueba
            signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiration
            };
        }

        [HttpGet("listarUsuarios/{activo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> listarUsuarios(bool activo)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!(await _usuarios.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var respuesta = await _usuarios.listarUsuarios(activo);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> modificarUsuarioActivo([FromBody] UsuarioPutDTO usuarioPutDTO)
        {
            var usuario = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!(await _usuarios.IsUserInRoleAsync(usuario!, Tipo_Usuario.Dungeon_Master.ToString())))
            {
                return Forbid();
            }

            var respuesta = await _usuarios.modificarUsuarioActivo(usuarioPutDTO.Usuario!, usuarioPutDTO.Activo);
            if (respuesta.Exitoso)
            {
                return Ok(respuesta.Resultado);
            }
            return BadRequest(respuesta.Mensaje);
        }
    }
}
