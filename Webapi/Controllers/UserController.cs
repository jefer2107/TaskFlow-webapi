using Application.DTOs.User;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) 
: ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("/users")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> FindAll()
    {
        try
        {
            IEnumerable<UserDTO> users = await _userService.FindAll();
            
            return Ok(users);
        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }
}
