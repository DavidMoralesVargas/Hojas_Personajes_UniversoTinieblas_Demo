using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Tipos_DepredadorController : GenericoController<Tipo_Depredador>
    {
        public Tipos_DepredadorController(IGenericoRepositorio<Tipo_Depredador> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
