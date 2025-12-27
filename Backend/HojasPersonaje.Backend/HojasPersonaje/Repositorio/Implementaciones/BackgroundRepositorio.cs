using FluentValidation;
using HojasPersonaje.Data;
using HojasPersonaje.Entidades;
using HojasPersonaje.Repositorio.Interfaces;

namespace HojasPersonaje.Repositorio.Implementaciones
{
    public class BackgroundRepositorio : IBackgroundRepositorio
    {
        private readonly IValidator<Background> _validator;
        private readonly ClaseContexto _contexto;

        public BackgroundRepositorio(IValidator<Background> validator, ClaseContexto contexto)
        {
            _validator = validator;
            _contexto = contexto;
        }

        public async Task<Background> AgregarBackgroundAsync(Background background)
        {
            var validationResult = await _validator.ValidateAsync(background);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            _contexto.Backgrounds.Add(background);
            await _contexto.SaveChangesAsync();
            return background;
        }
    }
}
