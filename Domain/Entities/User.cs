namespace Domain.Entities;

public class User : EntityModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public ICollection<Chore> Chores { get; set; } = [];
}
