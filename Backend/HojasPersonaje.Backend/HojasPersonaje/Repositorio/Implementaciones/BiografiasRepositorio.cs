using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class BiografiasRepositorio : GenericoRepositorio<Biografia>, IBiografiasRepositorio
    {
        private readonly IValidator<Biografia> _validator;
        private readonly ClaseContexto _contexto;

        public BiografiasRepositorio(IValidator<Biografia> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
