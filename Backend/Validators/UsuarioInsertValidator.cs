using System.Data;
using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class UsuarioInsertValidator : AbstractValidator<UsuarioInsertDto>
    {
        public UsuarioInsertValidator() { 
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.UserName).Length(2, 20).WithMessage("El nombre debe tener entre 2 y 20 caracteres");
            RuleFor(x => x.Email).NotNull().WithMessage("El email no puede ser null o estar en blanco");
            RuleFor(x => x.TipoUsuarioID).GreaterThan(0).WithMessage(x => "Error con el {PropertyName} enviado");

        }
    }
}
