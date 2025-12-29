using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class FlawsRepositorio : GenericoRepositorio<Flaw>, IFlawsRepositorio
    {
        private readonly IValidator<Flaw> _validator;
        private readonly ClaseContexto _contexto;

        public FlawsRepositorio(IValidator<Flaw> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
