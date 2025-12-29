using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class BackgroundRepositorio : GenericoRepositorio<Background>, IBackgroundRepositorio
    {
        private readonly IValidator<Background> _validator;
        private readonly ClaseContexto _contexto;

        public BackgroundRepositorio(IValidator<Background> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _validator = validator;
            _contexto = contexto;
        }

    }
}
