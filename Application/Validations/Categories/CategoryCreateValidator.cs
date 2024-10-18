using Application.DTOs.Category;
using FluentValidation;

namespace Application.Validations.Categories;

public class CategoryCreateValidator 
: AbstractValidator<CategoryInputCreateDTO>
{
    public CategoryCreateValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Valor não pode ser vazio")
        .MaximumLength(30).WithMessage("Os valores não podem ultrapassarem 30 caracteres");
    }
}
