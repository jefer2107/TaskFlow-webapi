using Application.DTOs.Chore;
using Application.Interfaces;
using Application.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChoreController(
    IChoreService choreService,
    IValidator<ChoreInputCreateDTO> createValidator,
    IValidator<ChoreInputUpdateDTO> updateValidator
) 
: ControllerBase
{
    private readonly IChoreService _choreService = choreService;
    private readonly IValidator<ChoreInputCreateDTO> _createValidator = createValidator;
    private readonly IValidator<ChoreInputUpdateDTO> _updateValidator = updateValidator;

    [HttpGet("/chores")]
    public async Task<ActionResult<IEnumerable<ChoreDTO>>> FindAll()
    {
        try
        {
            IEnumerable<ChoreDTO> chores = await _choreService.FindAll();
            
            return Ok(chores);
        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpGet("/chore/{id}/user/{userId}")]
    public async Task<ActionResult<IEnumerable<ChoreDTO>>> FindOne(int id, int userId)
    {
        try
        {

            ChoreDTO chore = await _choreService.FindOneByUser(id, userId);

            if(chore == null)
            {
                return BadRequest(new { Id = new[]{ $"Id:{id} não encontrado" } });
            }

            return Ok(chore);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPost("/chore")]
    public async Task<ActionResult<ChoreDTO>> Create([FromBody] ChoreInputCreateDTO body)
    {
        try
        {
            var statusValidator = new StatusValidator<ChoreInputCreateDTO>(_createValidator);
            var validationResult = await statusValidator
            .HandleFailure(ModelState, body);

            if(!validationResult)
                return BadRequest(ModelState);
    

            var result = await _choreService.Create(body);
            return Ok(result);

        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPatch("/chore/{id}")]
    public async Task<ActionResult<ChoreDTO>> Update(string id, [FromBody] ChoreInputUpdateDTO body)
    {
        
        try
        {
            var statusValidator = new StatusValidator<ChoreInputUpdateDTO>(_updateValidator);

            var validationResult = await statusValidator
            .HandleFailure(ModelState, body);

            if(!validationResult)
                return BadRequest(ModelState);

            if(!int.TryParse(id, out int newId))
                return BadRequest(new { Id = new[]{ "Id inválido" } });

            var chore = await _choreService.FindOne(newId);

            if(chore == null)
                return BadRequest(new { Id = new[]{ $"Usuário com este Id:{newId} não encontrado" } });


            var result = await _choreService.Update(newId, body);
            return Ok(result);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpDelete("/chore/{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            ChoreDTO choreToBeDeleted = await _choreService
            .FindOne(id);

            if(choreToBeDeleted == null) 
                return BadRequest(new { Id = new[]{ $"Id:{id} não encontrado" } });

            bool result = await _choreService.Delete(id);
            return Ok(result);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }
}
