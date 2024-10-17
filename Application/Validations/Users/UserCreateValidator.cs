using Application.DTOs.User;
using FluentValidation;

namespace Application.Validations.Users;

public class UserCreateValidator
: AbstractValidator<UserInputCreateDTO>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Valor não pode ser vazio")
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres");

        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Valor não pode ser vazio")
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres")
        .EmailAddress().WithMessage("Formato de Email inválido");

        RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Valor não pode ser vazio")
        .Length(8,8).WithMessage("Valor deve ter 8 cracteres");
    }
}
