using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Notas_AdicionalesRepositorio : GenericoRepositorio<Nota_Adicional>, INotas_AdicionalesRepositorio
    {
        private readonly IValidator<Nota_Adicional> _validator;
        private readonly ClaseContexto _contexto;

        public Notas_AdicionalesRepositorio(IValidator<Nota_Adicional> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
