using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using Microsoft.AspNetCore.Identity;

namespace HojasPersonaje.Repositorio.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<Usuario> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(Usuario user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(Usuario user, string roleName);
        Task<bool> IsUserInRoleAsync(Usuario user, string roleName);
        Task<SignInResult> LoginAsync(LoginDTO model);
        Task LogoutAsync();
    }
}
