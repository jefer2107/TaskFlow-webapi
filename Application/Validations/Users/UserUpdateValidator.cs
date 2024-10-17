using Application.DTOs.User;
using FluentValidation;

namespace Application.Validations.Users;

public class UserUpdateValidator 
: AbstractValidator<UserInputUpdateDTO>
{
    public UserUpdateValidator()
    {
        RuleFor(x => x.Name)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres");

        RuleFor(x => x.Email)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres")
        .EmailAddress().WithMessage("Formato de Email inválido");

        RuleFor(x => x.Password)
        .Length(8,8).WithMessage("Valor deve ter 8 cracteres");
    }
}
