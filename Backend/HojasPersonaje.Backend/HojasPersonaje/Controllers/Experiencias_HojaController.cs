using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Experiencias_HojaController : GenericoController<Experiencia_Hoja>
    {
        public Experiencias_HojaController(IGenericoRepositorio<Experiencia_Hoja> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
