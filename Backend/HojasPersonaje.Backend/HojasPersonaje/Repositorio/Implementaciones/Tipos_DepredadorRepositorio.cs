using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Tipos_DepredadorRepositorio : GenericoRepositorio<Tipo_Depredador>, ITipos_DepredadorRepositorio
    {
        private readonly IValidator<Tipo_Depredador> _validator;
        private readonly ClaseContexto _contexto;

        public Tipos_DepredadorRepositorio(IValidator<Tipo_Depredador> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
