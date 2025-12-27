using FluentValidation;

namespace HojasPersonaje.Entidades.ValidacionesDatos
{
    public class BackgroundValidator : AbstractValidator<Background>
    {
        public BackgroundValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre del background es obligatorio");
            RuleFor(x => x.Nivel)
                .GreaterThanOrEqualTo(0)
                .InclusiveBetween(1, 5)
                .WithMessage("El nivel debe tener un valor entre 1 y 5");
        }
    }
}
