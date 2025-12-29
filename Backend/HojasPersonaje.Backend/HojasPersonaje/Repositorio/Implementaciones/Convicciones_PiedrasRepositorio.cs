using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Convicciones_PiedrasRepositorio : GenericoRepositorio<Conviccion_Piedras>, IConvicciones_PiedrasRepositorio
    {
        private readonly IValidator<Conviccion_Piedras> _validator;
        private readonly ClaseContexto _contexto;

        public Convicciones_PiedrasRepositorio(IValidator<Conviccion_Piedras> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
