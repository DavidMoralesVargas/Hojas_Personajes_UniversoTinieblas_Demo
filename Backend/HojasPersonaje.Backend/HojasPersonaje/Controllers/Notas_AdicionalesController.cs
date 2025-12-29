using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Notas_AdicionalesController : GenericoController<Nota_Adicional>
    {
        public Notas_AdicionalesController(IGenericoRepositorio<Nota_Adicional> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
