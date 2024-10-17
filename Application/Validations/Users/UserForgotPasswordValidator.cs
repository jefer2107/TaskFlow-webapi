using Application.DTOs.User;
using FluentValidation;

namespace Application.Validations.Users;

public class UserForgotPasswordValidator 
: AbstractValidator<ForgotPasswordDTO>
{
    public UserForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres")
        .NotEmpty().WithMessage("Email não pode ser vazio")
        .EmailAddress().WithMessage("Formato de Email inválido");

        RuleFor(x => x.Domain)
        .NotEmpty().WithMessage("Domínio não pode ser vazio");
    }
}
