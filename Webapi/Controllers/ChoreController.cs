using Application.DTOs.Chore;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChoreController(IChoreService choreService) 
: ControllerBase
{
    private readonly IChoreService _choreService = choreService;

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
}
