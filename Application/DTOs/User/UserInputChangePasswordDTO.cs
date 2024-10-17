namespace Application.DTOs.User;

public class UserInputChangePasswordDTO
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
    public required string Email { get; set; }
}
