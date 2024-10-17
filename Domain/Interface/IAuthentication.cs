using Domain.Entities;
using Domain.Objects;

namespace Domain.Interface;

public interface IAuthentication
{
    Task<AuthResult> SignIn(User user);
    Task<UserToken> TokenGenerate(User user);
}
