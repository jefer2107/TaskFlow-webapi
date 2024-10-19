using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Domain.Interface;
using Domain.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Data.Identity;

public class Authentication(
    IUserRepository userRepository,
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager,
    IConfiguration configuration
) : IAuthentication
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public async Task<AuthResult> SignIn(User user)
    {
        try
        {
            var userByEmail = await _userManager
            .FindByEmailAsync(user.Email);

            if(userByEmail == null)
                return new AuthResult{ Success = false, ErrorMessage = "Usuário não encontrado" };

            var resultRegister = await _signInManager.PasswordSignInAsync(
                user.Email,
                user.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if(!resultRegister.Succeeded)
                return new AuthResult{ Success = false, ErrorMessage = "Credenciais inválidas." };

            var token = await TokenGenerate(user);

            return new AuthResult{ Success = true, UserToken = token };
        }
        catch (Exception error)
        {
            
            throw new Exception(
                $"Error authenticate service in method Authenticate: {error}"
            );
        }
    }

    public async Task<UserToken> TokenGenerate(User user)
    {
        try
        {
            User userByEmail = await _userRepository
            .FindByEmail(user.Email);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userByEmail.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", userByEmail.Id.ToString()),
                new Claim("Name", userByEmail.Name),
                new Claim("Email", userByEmail.Email)
            };

            var secretKey = _configuration["jwt:Key"];
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey ?? ""));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var timeExpirations = _configuration["TokenConfiguration:ExpireHours"];
            var expires = DateTime.UtcNow.AddHours(double.Parse(timeExpirations ?? ""));

            JwtSecurityToken token = new(
                issuer:_configuration["TokenConfiguration:Issuer"],
                audience:_configuration["TokenConfiguration:Audience"],
                claims:claims,
                expires:expires,
                signingCredentials: credentials
            );

            UserToken userToken = new()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires,
                Message = "Token Jwt Ok"
            };

            return userToken;
        }
        catch (Exception error)
        {
            
            throw new Exception(
                $"Error authenticate service in method TokenGenerate: {error}"
            );
        }
    }

}
