using FluentValidation;

namespace HojasPersonaje.Entidades.ValidacionesDatos
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nombre_Usuario)
                .NotEmpty()
                .WithMessage("El nombre del usuario es obligatorio");;
        }
    }
}
