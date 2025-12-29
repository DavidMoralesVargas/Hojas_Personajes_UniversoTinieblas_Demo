using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class WeaponsRepositorio : GenericoRepositorio<Weapon>, IWeaponsRepositorio
    {
        private readonly IValidator<Weapon> _validator;
        private readonly ClaseContexto _contexto;

        public WeaponsRepositorio(IValidator<Weapon> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

    }
}
