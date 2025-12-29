using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class HabilidadesPersonajesRepositorio : GenericoRepositorio<HabilidadesPersonaje>, IHabilidadesPersonajesRepositorio
    {
        private readonly IValidator<HabilidadesPersonaje> _validator;
        private readonly ClaseContexto _contexto;

        public HabilidadesPersonajesRepositorio(IValidator<HabilidadesPersonaje> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
