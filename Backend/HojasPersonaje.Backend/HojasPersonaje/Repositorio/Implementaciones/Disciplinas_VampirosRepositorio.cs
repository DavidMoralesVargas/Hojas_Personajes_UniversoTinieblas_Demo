using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Disciplinas_VampirosRepositorio : GenericoRepositorio<Disciplina_Vampiro>, IDisciplinas_VampirosRepositorio
    {
        private readonly IValidator<Disciplina_Vampiro> _validator;
        private readonly ClaseContexto _contexto;

        public Disciplinas_VampirosRepositorio(IValidator<Disciplina_Vampiro> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

        public async Task<ActionResponse<ICollection<Disciplina_Vampiro>>> ComboAllAsync(int idVampiro)
        {
            try
            {
                return new ActionResponse<ICollection<Disciplina_Vampiro>>
                {
                    Mensaje = "Consulta exitosa",
                    Exitoso = true,
                    Resultado = await _contexto.Disciplinas_Vampiro.Where(x => x.VampiroId == idVampiro)
                                                                   .Include(d => d.Disciplina)
                                                                   .ThenInclude(dh => dh!.Habilidades_Disciplina)
                                                                   .ToListAsync()
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<ICollection<Disciplina_Vampiro>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                }; 
            }
        }
    }
}
