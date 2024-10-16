using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserRepository userRepository) 
: ControllerBase
{
    private readonly IUserRepository _userReporitory = userRepository;

    [HttpGet("/users")]
    public async Task<ActionResult<IEnumerable<User>>> FindAll()
    {
        try
        {
            IEnumerable<User> users = await _userReporitory.FindAll();
            
            return Ok(users);
        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }
}
