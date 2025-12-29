using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Atributos_Hoja_PersonajeRepositorio : GenericoRepositorio<Atributo_Hoja_Personaje>, IAtributos_Hoja_PersonajeRepositorio
    {
        private readonly IValidator<Atributo_Hoja_Personaje> _validator;
        private readonly ClaseContexto _contexto;

        public Atributos_Hoja_PersonajeRepositorio(IValidator<Atributo_Hoja_Personaje> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }
    }
}
