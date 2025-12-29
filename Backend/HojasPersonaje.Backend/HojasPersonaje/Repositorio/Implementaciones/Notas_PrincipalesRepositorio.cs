using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Notas_PrincipalesRepositorio : GenericoRepositorio<Notas_Principales>, INotas_PrincipalesRepositorio
    {
        private readonly IValidator<Notas_Principales> _validator;
        private readonly ClaseContexto _contexto;

        public Notas_PrincipalesRepositorio(IValidator<Notas_Principales> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
