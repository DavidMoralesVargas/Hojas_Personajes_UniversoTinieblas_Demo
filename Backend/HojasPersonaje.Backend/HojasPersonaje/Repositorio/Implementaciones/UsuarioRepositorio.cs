using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.DTOs;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class UsuarioRepositorio : GenericoRepositorio<Usuario>, IUsuariosRepositorio
    {
        private readonly IValidator<Usuario> _validator;
        private readonly ClaseContexto _contexto;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioRepositorio(IValidator<Usuario> validator, ClaseContexto contexto, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Usuario> signInManager) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> AddUserAsync(Usuario user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(Usuario user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<Usuario> GetUserAsync(string email)
        {
            var user = await _contexto.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }

        public async Task<bool> IsUserInRoleAsync(string user, string roleName)
        {
            var userFind = await GetUserAsync(user);
            if (userFind == null)
            {
                return false;
            }

            return await _userManager.IsInRoleAsync(userFind, roleName);
        }
    }
}
