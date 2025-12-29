using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class MeritosRepositorio : GenericoRepositorio<Merito>, IMeritosRepositorio
    {
        private readonly IValidator<Merito> _validator;
        private readonly ClaseContexto _contexto;

        public MeritosRepositorio(IValidator<Merito> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

    }
}
