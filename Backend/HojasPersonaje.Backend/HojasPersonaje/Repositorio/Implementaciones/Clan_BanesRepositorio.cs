using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Clan_BanesRepositorio : GenericoRepositorio<Clan_Bane>, IClan_BanesRepositorio
    {
        private readonly IValidator<Clan_Bane> _validator;
        private readonly ClaseContexto _contexto;

        public Clan_BanesRepositorio(IValidator<Clan_Bane> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
