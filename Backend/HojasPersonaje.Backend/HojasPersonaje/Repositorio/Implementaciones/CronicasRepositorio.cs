using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class CronicasRepositorio : GenericoRepositorio<Cronica>, ICronicasRepositorio
    {
        private readonly IValidator<Cronica> _validator;
        private readonly ClaseContexto _contexto;

        public CronicasRepositorio(IValidator<Cronica> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
