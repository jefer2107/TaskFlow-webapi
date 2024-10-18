using Application.DTOs.Chore;
using FluentValidation;

namespace Application.Validations.Chores;

public class ChoreUpdateValidator 
: AbstractValidator<ChoreInputUpdateDTO>
{
    public ChoreUpdateValidator()
    {
        RuleFor(x => x.Title)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres");
    }
}
