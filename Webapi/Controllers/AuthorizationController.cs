using Application.DTOs.User;
using Application.Interfaces;
using Domain.Objects;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController(
    IAuthorizationService authorizationService
)
: ControllerBase
{
    private readonly IAuthorizationService _authorizationService = authorizationService;

    [HttpPost("/login")]
    public async Task<ActionResult<AuthResult>> Login([FromBody] UserInputAuthenticateDTO user)
    {
        try
        {

            AuthResult authResult = await _authorizationService.Login(user);
            return Ok(authResult);
        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
            return StatusCode(500, "Algo inesperado ocorreu");
        }
    }
}
