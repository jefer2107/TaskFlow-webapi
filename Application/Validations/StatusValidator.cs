using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Validations;

public class StatusValidator<T>
{
    private IValidator<T> _validator;
    public StatusValidator(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task<bool> HandleFailure(ModelStateDictionary modelState, T body)
    {
        
        var validationResult = await _validator.ValidateAsync(body);

        if(!validationResult.IsValid)
        {
            foreach(var validate in validationResult.Errors)
            {
                modelState.AddModelError(validate.PropertyName, validate.ErrorMessage);
            }

            return false;

        }else return true;
    }
}
