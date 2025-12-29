using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Especialidades_HabilidadesRepositorio : GenericoRepositorio<Especialidad_Habilidad>, IEspecialidades_HabilidadesRepositorio
    {
        private readonly IValidator<Especialidad_Habilidad> _validator;
        private readonly ClaseContexto _contexto;

        public Especialidades_HabilidadesRepositorio(IValidator<Especialidad_Habilidad> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
