using Application.DTOs.User;
using Application.Interfaces;
using Application.Validations;
using Domain.Entities;
using Domain.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
    IUserService userService,
    IValidator<UserInputCreateDTO> createValidator,
    IValidator<UserInputUpdateDTO> updateValidator,
    IValidator<UserInputChangePasswordDTO> changePasswordValidator,
    IValidator<ResetPasswordDTO> resetPasswordValidator,
    IValidator<ForgotPasswordDTO> forgotPasswordValidator
) 
: ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IValidator<UserInputCreateDTO> _createValidator = createValidator;
    private readonly IValidator<UserInputUpdateDTO> _updateValidator = updateValidator;
    private readonly IValidator<UserInputChangePasswordDTO> _changePasswordValidator = changePasswordValidator;
    private readonly IValidator<ResetPasswordDTO> _resetPasswordValidator = resetPasswordValidator;
    private readonly IValidator<ForgotPasswordDTO> _forgotPasswordValidator = forgotPasswordValidator;

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

    [HttpGet("/user/{id}")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> FindOne(int id)
    {
        try
        {

            UserDTO user = await _userService.FindOne(id);

            if(user == null)
            {
                return BadRequest(new { Id = new[]{ $"Id:{id} não encontrado" } });
            }

            return Ok(user);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPost("/user")]
    public async Task<ActionResult<UserDTO>> Create([FromBody] UserInputCreateDTO body)
    {
        try
        {
            var statusValidator = new StatusValidator<UserInputCreateDTO>(_createValidator);
            var validationResult = await statusValidator
            .HandleFailure(ModelState, body);

            if(!validationResult)
                return BadRequest(ModelState);
            
            var users = await _userService.FindAll();
            var userByEmail = users.SingleOrDefault(x => x.Email == body.Email);

            if(userByEmail != null)
                return StatusCode(409, new{ Email = new[]{ $"Já existe cadastro com este Email:{body.Email}" } });

            var result = await _userService.Create(body);
            return Ok(result);

        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPatch("/user/{id}")]
    public async Task<ActionResult<UserDTO>> Update(string id, [FromBody] UserInputUpdateDTO body)
    {
        
        try
        {
            var statusValidator = new StatusValidator<UserInputUpdateDTO>(_updateValidator);

            var validationResult = await statusValidator
            .HandleFailure(ModelState, body);

            if(!validationResult)
                return BadRequest(ModelState);

            if(!int.TryParse(id, out int newId))
                return BadRequest(new { Id = new[]{ "Id inválido" } });

            var user = await _userService.FindOne(newId);

            if(user == null)
                return BadRequest(new { Id = new[]{ $"Usuário com este Id:{newId} não encontrado" } });

            var users = await _userService.FindAll();
            var userByEmail = users.SingleOrDefault(x => x.Email == body.Email && newId != x.Id);

            if(body.Email != null && userByEmail != null)
                return StatusCode(409, new{ Email = new[]{ $"Já existe cadastro com este Email:{body.Email}" } });

            if(body.Password != null) 
                return StatusCode(403, new{ Password = new[]{ "Não é permitido alterar a senha nesse método" } });

            var result = await _userService.Update(newId, body);
            return Ok(result);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpDelete("/user/{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            UserDTO userToBeDeleted = await _userService
            .FindOne(id);

            if(userToBeDeleted == null) 
                return BadRequest(new { Id = new[]{ $"Id:{id} não encontrado" } });

            bool result = await _userService.Delete(id);
            return Ok(result);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }
    
    [HttpPut("/user/password-change")]
    public async Task<ActionResult<bool>> ChangePassword([FromBody] UserInputChangePasswordDTO user)
    {
        try
        {
            var statusValidator = new StatusValidator<UserInputChangePasswordDTO>(_changePasswordValidator);

            var validationResult = await statusValidator
            .HandleFailure(ModelState, user);

            if(!validationResult)
                return BadRequest(ModelState);
            
            bool result = await _userService.ChangePassword(user);
            return Ok(result);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);

            if(error.Message == $"Error in user services method ChangePassword: Password:{user.CurrentPassword} not found")
                return BadRequest( new{ CurrentPassword = new[]{ "A senha não confere" } } );

            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }

    }

    [HttpPost("/user/forgot-password")]
    public async Task<ActionResult<string>> ForgotMyPassword([FromBody] ForgotPasswordDTO model)
    {
        try
        {
            var statusValidator = new StatusValidator<ForgotPasswordDTO>(_forgotPasswordValidator);

            var validationResult = await statusValidator
            .HandleFailure(ModelState, model);

            var url = await _userService.ForgotMyPassword(model);

            return Ok(url);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }

    [HttpPost("/user/reset-password")]
    public async Task<ActionResult<bool>> ResetPassword([FromBody] ResetPasswordDTO model)
    {
        try
        {
            var statusValidator = new StatusValidator<ResetPasswordDTO>(_resetPasswordValidator);
            var validationResult = await statusValidator
            .HandleFailure(ModelState, model);

            var result = await _userService.ResetPassword(model);
            return Ok(result);
        }
        catch (Exception error)
        {
            
            Console.WriteLine(error.Message);
            return StatusCode(500, new{ InternalServerError = new[]{ "Algo inesperado ocorreu" } });
        }
    }
}
