using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Lista_AmigosRepositorio : GenericoRepositorio<Lista_Amigos>, ILista_AmigosRepositorio
    {
        private readonly IValidator<Lista_Amigos> _validator;
        private readonly ClaseContexto _contexto;

        public Lista_AmigosRepositorio(IValidator<Lista_Amigos> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

    }
}
