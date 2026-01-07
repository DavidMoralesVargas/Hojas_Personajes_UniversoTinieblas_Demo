using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DisciplinasController : GenericoController<Disciplina>
    {
        public DisciplinasController(IGenericoRepositorio<Disciplina> genericoRepositorio) : base(genericoRepositorio)
        {

        }
    }
}
