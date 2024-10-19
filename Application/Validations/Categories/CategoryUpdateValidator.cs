using Application.DTOs.Category;
using FluentValidation;

namespace Application.Validations.Categories;

public class CategoryUpdateValidator 
: AbstractValidator<CategoryInputUpdateDTO>
{
    public CategoryUpdateValidator()
    {
        RuleFor(x => x.Name)
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres");
    }
}
