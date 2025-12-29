using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class HabilidadesRepositorio : GenericoRepositorio<Habilidad>, IHabilidadesRepositorio
    {
        private readonly IValidator<Habilidad> _validator;
        private readonly ClaseContexto _contexto;

        public HabilidadesRepositorio(IValidator<Habilidad> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

    }
}
