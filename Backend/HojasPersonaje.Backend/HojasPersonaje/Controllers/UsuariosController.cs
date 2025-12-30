using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
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
    }
}
