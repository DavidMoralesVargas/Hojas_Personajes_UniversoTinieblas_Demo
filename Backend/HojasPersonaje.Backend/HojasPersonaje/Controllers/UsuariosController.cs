using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuariosController : GenericoController<Usuario>
    {
        public UsuariosController(IGenericoRepositorio<Usuario> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
