using Application.DTOs;
using Application.DTOs.User;
using FluentValidation;

namespace Application.Validations.Users;

public class UserPasswordChangeValidator 
: AbstractValidator<UserInputChangePasswordDTO>
{
    public UserPasswordChangeValidator()
    {
        RuleFor(x => x.Email)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres")
        .EmailAddress().WithMessage("Formato de Email inválido")
        .NotEmpty().WithMessage("Valor não pode ser vazio");

        RuleFor(x => x.CurrentPassword)
        .Length(8,8).WithMessage("Valor deve ter 8 cracteres")
        .NotEmpty().WithMessage("Valor não pode ser vazio");

        RuleFor(x => x.NewPassword)
        .Length(8,8).WithMessage("Valor deve ter 8 cracteres")
        .NotEmpty().WithMessage("Valor não pode ser vazio");
    }
}
