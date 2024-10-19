namespace Domain.Entities;

public class Category : EntityModel
{
    public int? UserId { get; set; }
    public required string Name { get; set; }
    public ICollection<Chore> Chores { get; set; }
    public User User { get; set; }
}
