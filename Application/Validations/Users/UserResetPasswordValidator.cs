using Application.DTOs;
using Application.DTOs.User;
using FluentValidation;

namespace Application.Validations.Users;

public class UserResetPasswordValidator : AbstractValidator<ResetPasswordDTO>
{
    public UserResetPasswordValidator()
    {
        RuleFor(x => x.Email)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres")
        .NotEmpty().WithMessage("Email não pode ser vazio")
        .EmailAddress().WithMessage("Formato de Email inválido");

        RuleFor(x => x.Token)
        .NotEmpty().WithMessage("Token não pode ser vazio");

        RuleFor(x => x.newPassword)
        .NotEmpty().WithMessage("Password não pode ser vazio")
        .Length(8,8).WithMessage("Valor deve ter 8 cracteres");
    }
}
