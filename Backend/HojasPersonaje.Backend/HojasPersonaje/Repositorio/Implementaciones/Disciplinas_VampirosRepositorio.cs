using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Disciplinas_VampirosRepositorio : GenericoRepositorio<Disciplina_Vampiro>, IDisciplinas_VampirosRepositorio
    {
        private readonly IValidator<Disciplina_Vampiro> _validator;
        private readonly ClaseContexto _contexto;

        public Disciplinas_VampirosRepositorio(IValidator<Disciplina_Vampiro> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
