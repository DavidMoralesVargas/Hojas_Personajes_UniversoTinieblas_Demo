using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Listas_AmigosController : GenericoController<Lista_Amigos>
    {
        public Listas_AmigosController(IGenericoRepositorio<Lista_Amigos> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
