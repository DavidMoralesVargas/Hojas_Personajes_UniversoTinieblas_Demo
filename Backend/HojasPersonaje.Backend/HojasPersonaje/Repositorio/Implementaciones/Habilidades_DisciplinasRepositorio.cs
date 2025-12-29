using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Habilidades_DisciplinasRepositorio : GenericoRepositorio<Habilidades_Disciplina>, IHabilidades_DisciplinasRepositorio
    {
        private readonly IValidator<Habilidades_Disciplina> _validator;
        private readonly ClaseContexto _contexto;

        public Habilidades_DisciplinasRepositorio(IValidator<Habilidades_Disciplina> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
