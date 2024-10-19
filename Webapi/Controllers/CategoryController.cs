using Application.DTOs.Category;
using Application.Interfaces;
using Application.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(
    ICategoryService categoryService,
    IValidator<CategoryInputCreateDTO> createValidator,
    IValidator<CategoryInputUpdateDTO> updateValidator
) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IValidator<CategoryInputCreateDTO> _createValidator = createValidator;
    private readonly IValidator<CategoryInputUpdateDTO> _updateValidator = updateValidator;

    [HttpGet("/categories")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> FindAll([FromQuery] int userId)
    {
        try
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.FindAllByUser(userId);
            
            return Ok(categories);
        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPost("/category")]
    public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryInputCreateDTO body)
    {
        try
        {
            var statusValidator = new StatusValidator<CategoryInputCreateDTO>(_createValidator);
            var validationResult = await statusValidator
            .HandleFailure(ModelState, body);

            if(!validationResult)
                return BadRequest(ModelState);
    

            var result = await _categoryService.Create(body);
            return Ok(result);

        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPatch("/category/{id}")]
    public async Task<ActionResult<CategoryDTO>> Update(string id, [FromBody] CategoryInputUpdateDTO body)
    {
        
        try
        {
            var statusValidator = new StatusValidator<CategoryInputUpdateDTO>(_updateValidator);

            var validationResult = await statusValidator
            .HandleFailure(ModelState, body);

            if(!validationResult)
                return BadRequest(ModelState);

            if(!int.TryParse(id, out int newId))
                return BadRequest(new { Id = new[]{ "Id inválido" } });

            var chore = await _categoryService.FindOne(newId);

            if(chore == null)
                return BadRequest(new { Id = new[]{ $"Usuário com este Id:{newId} não encontrado" } });


            var result = await _categoryService.Update(newId, body);
            return Ok(result);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpDelete("/category/{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            CategoryDTO categoryToBeDeleted = await _categoryService
            .FindOne(id);

            if(categoryToBeDeleted == null) 
                return BadRequest(new { Id = new[]{ $"Id:{id} não encontrado" } });

            bool result = await _categoryService.Delete(id);
            return Ok(result);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }
}
