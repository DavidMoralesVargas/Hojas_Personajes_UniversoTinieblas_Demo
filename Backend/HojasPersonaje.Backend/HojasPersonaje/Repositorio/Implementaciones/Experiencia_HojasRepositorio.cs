using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Experiencia_HojasRepositorio : GenericoRepositorio<Experiencia_Hoja>, IExperiencia_HojasRepositorio
    {
        private readonly IValidator<Experiencia_Hoja> _validator;
        private readonly ClaseContexto _contexto;

        public Experiencia_HojasRepositorio(IValidator<Experiencia_Hoja> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

    }
}
