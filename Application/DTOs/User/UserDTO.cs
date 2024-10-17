namespace Application.DTOs.User;

public class UserDTO : EntityModelDTO
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
