using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HojasPersonaje.Data
{
    public class SeedDb
    {
        private readonly ClaseContexto _contexto;
        private readonly IUsuariosRepositorio _usuarios;

        public SeedDb(ClaseContexto contexto, IUsuariosRepositorio usuario)
        {
            _contexto = contexto;
            _usuarios = usuario;
        }

        public async Task SeedAsync()
        {
            await _contexto.Database.EnsureCreatedAsync();
            await CrearRolesAsync();
            await CrearUsuarioAsync("DM", "dm@yopmail.com", "322 311 4620", Tipo_Usuario.Dungeon_Master);
        }

        private async Task CrearRolesAsync()
        {
            await _usuarios.CheckRoleAsync(Tipo_Usuario.Dungeon_Master.ToString());
            await _usuarios.CheckRoleAsync(Tipo_Usuario.Jugador.ToString());
        }

        private async Task CrearUsuarioAsync(string nombreUsuario, string email, string telefono, Tipo_Usuario tipo_Usuario)
        {
            var usuario = await _usuarios.GetUserAsync(email);
            if (usuario == null)
            {
                usuario = new Usuario()
                {
                    Nombre_Usuario = nombreUsuario,
                    Email = email,
                    UserName = email,
                    PhoneNumber = telefono,
                    Tipo_Usuario = tipo_Usuario
                };
                await _usuarios.AddUserAsync(usuario, "123456");
                await _usuarios.AddUserToRoleAsync(usuario, tipo_Usuario.ToString());
            }
        }
    }
}
