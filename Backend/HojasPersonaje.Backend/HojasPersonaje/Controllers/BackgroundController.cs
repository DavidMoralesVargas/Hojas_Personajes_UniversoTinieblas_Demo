using FluentValidation;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Implementaciones;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BackgroundController : GenericoController<Background>
    {

        public BackgroundController(IGenericoRepositorio<Background> genericoRepositorio) : base(genericoRepositorio)
        {

        }


    }
}
