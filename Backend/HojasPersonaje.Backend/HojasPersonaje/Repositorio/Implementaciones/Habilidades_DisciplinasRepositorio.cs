using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Helpers;
using HojasPersonaje.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class Habilidades_DisciplinasRepositorio : GenericoRepositorio<Habilidades_Disciplina>, IHabilidades_DisciplinasRepositorio
    {
        private readonly IValidator<Habilidades_Disciplina> _validator;
        private readonly ClaseContexto _contexto;

        public Habilidades_DisciplinasRepositorio(IValidator<Habilidades_Disciplina> validator, ClaseContexto contexto) : base(validator, contexto)
        {
            _contexto = contexto;
            _validator = validator;
        }

        public async Task<ActionResponse<IEnumerable<Habilidades_Disciplina>>> ObtenerTodosPorIdAsync(int id)
        {
            try
            {
                return new ActionResponse<IEnumerable<Habilidades_Disciplina>>
                {
                    Mensaje = "Registros encontrados con éxito",
                    Exitoso = true,
                    Resultado = await _contexto.Habilidades_Disciplina.Where(x => x.DisciplinaId == id).ToListAsync()
                };
            }
            catch(Exception ex)
            {
                return new ActionResponse<IEnumerable<Habilidades_Disciplina>>
                {
                    Exitoso = false,
                    Mensaje = ex.Message
                };
            }
        }
    }
}
