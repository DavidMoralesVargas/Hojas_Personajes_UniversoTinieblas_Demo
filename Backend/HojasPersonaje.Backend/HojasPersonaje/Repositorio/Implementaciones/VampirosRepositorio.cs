using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class VampirosRepositorio : GenericoRepositorio<Vampiro>, IVampirosRepositorio
    {
        private readonly IValidator<Vampiro> _validator;
        private readonly ClaseContexto _contexto;

        public VampirosRepositorio(IValidator<Vampiro> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

        //Verificar que un registro se encuentre en la base de datos por nombre
        public async Task<ActionResponse<bool>> verificarExistencia(string nombreVampiro)
        {
            var existe = await _contexto.Vampiros.FirstOrDefaultAsync(v => v.Nombre == nombreVampiro); //Filtra el nombre
            if (existe == null)  //Valida la existencia del registro
            {
                //Si es nulo, retorna false
                return new ActionResponse<bool>
                {
                    Exitoso = false,
                    Resultado = false,
                    Mensaje = "El vampiro no existe."
                };
            }
            //Si se encontró el registro, retorna true
            return new ActionResponse<bool>
            {
                Exitoso = true,
                Resultado = true,
                Mensaje = "El vampiro existe."
            };
        }
    }
}
