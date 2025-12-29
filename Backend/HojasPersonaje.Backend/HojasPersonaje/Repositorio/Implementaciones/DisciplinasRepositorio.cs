using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class DisciplinasRepositorio : GenericoRepositorio<Disciplina>, IDisciplinasRepositorio
    {
        private readonly IValidator<Disciplina> _validator;
        private readonly ClaseContexto _contexto;

        public DisciplinasRepositorio(IValidator<Disciplina> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

    }
}
