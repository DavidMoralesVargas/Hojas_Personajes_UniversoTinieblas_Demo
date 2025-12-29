using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Atributos_Hoja_PersonajeController : GenericoController<Atributo_Hoja_Personaje>
    {
        public Atributos_Hoja_PersonajeController(IGenericoRepositorio<Atributo_Hoja_Personaje> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
