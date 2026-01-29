using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ActionResponse<ICollection<Tipo_Depredador>>> ComboAsync()
        {
            try
            {
                return new ActionResponse<ICollection<Tipo_Depredador>>
                {
                    Mensaje = "Consulta exitosa",
                    Exitoso = true,
                    Resultado = await _contexto.Tipos_Depredador.ToListAsync()
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<ICollection<Tipo_Depredador>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
