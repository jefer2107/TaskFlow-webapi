using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Objects;

namespace Application.Services;

public class AuthorizationService(IAuthentication authentication, IMapper mapper)
: IAuthorizationService
{
    private readonly IAuthentication _authentication = authentication;
    private readonly IMapper _mapper = mapper;
    public async Task<AuthResult> Login(UserInputAuthenticateDTO model)
    {
        try
        {
            User user = _mapper.Map<User>(model);
            AuthResult authResult = await _authentication.SignIn(user);

            return authResult;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in AuthorizationService method Login: {error.InnerException}"
                );

            throw new Exception(
                $"Error in AuthorizationService method Login: {error.Message}"
            );
        }
    }

}
