using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Hojas_PersonajeRepositorio : GenericoRepositorio<Hoja_Personaje>, IHojas_PersonajeRepositorio
    {
        private readonly IValidator<Hoja_Personaje> _validator;
        private readonly ClaseContexto _contexto;

        public Hojas_PersonajeRepositorio(IValidator<Hoja_Personaje> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
