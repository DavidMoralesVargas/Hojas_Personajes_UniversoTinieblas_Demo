using HojasPersonaje.Entidades;

namespace HojasPersonaje.DTOs
{
    public class UsuarioDTO : Usuario
    {
        public string? Contraseña { get; set; }
        public string? ContraseñaConfirmacion { get; set; }
    }
}
