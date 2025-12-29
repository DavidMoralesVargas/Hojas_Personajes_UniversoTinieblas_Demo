using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class PosesionesRepositorio : GenericoRepositorio<Posesiones>,IPosesionesRepositorio
    {
        private readonly IValidator<Posesiones> _validator;
        private readonly ClaseContexto _contexto;

        public PosesionesRepositorio(IValidator<Posesiones> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
