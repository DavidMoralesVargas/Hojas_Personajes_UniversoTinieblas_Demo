using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Hojas_PersonajeRepositorio : GenericoRepositorio<Hoja_Personaje>, IHojas_PersonajeRepositorio
    {
        private readonly IValidator<Hoja_Personaje> _validator;
        private readonly ClaseContexto _contexto;
       

        public Hojas_PersonajeRepositorio(IValidator<Hoja_Personaje> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

        public async Task<ActionResponse<Hoja_Personaje>> BuscarPorCronicaIdAsync(int cronicaId, string idUsuario)
        {
            try
            {
                var cronica = await _contexto.Cronicas.FindAsync(cronicaId);
                if (cronica == null)
                {
                    return new ActionResponse<Hoja_Personaje>
                    {
                        Exitoso = false,
                        Mensaje = "La crónica no existe."
                    };
                }

                return new ActionResponse<Hoja_Personaje>
                {
                    Exitoso = true,
                    Mensaje = "Búsqueda exitosa.",
                    Resultado = await _contexto.Hojas_Personaje
                        .FirstOrDefaultAsync(h => h.CronicaId == cronicaId && h.JugadorId == idUsuario)
                };

            }
            catch(Exception ex)
            {
                return new ActionResponse<Hoja_Personaje>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
