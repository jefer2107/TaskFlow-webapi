using Application.DTOs.Chore;
using FluentValidation;

namespace Application.Validations.Chores;

public class ChoreCreateValidator 
: AbstractValidator<ChoreInputCreateDTO>
{
    public ChoreCreateValidator()
    {
        RuleFor(x => x.UserId)
        .NotNull().WithMessage("Valor não pode ser nulo");

        RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Valor não pode ser vazio")
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres");
    }
}
