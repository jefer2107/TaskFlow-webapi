using Application.DTOs.User;
using Domain.Objects;

namespace Application.Interfaces;

public interface IAuthorizationService
{
    Task<AuthResult> Login(UserInputAuthenticateDTO user);
}
