using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Principios_CronicaRepositorio : GenericoRepositorio<Principio_Cronica>, IPrincipios_CronicaRepositorio
    {
        private readonly IValidator<Principio_Cronica> _validator;
        private readonly ClaseContexto _contexto;

        public Principios_CronicaRepositorio(IValidator<Principio_Cronica> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
