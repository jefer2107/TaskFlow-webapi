namespace Application.DTOs.User;

public class UserInputAuthenticateDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
