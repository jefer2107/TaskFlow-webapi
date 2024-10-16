namespace Domain.Entities;

public class Category : EntityModel
{
    public required int TaskId { get; set; }
    public required string Name { get; set; }
    public ICollection<Chore> Tasks { get; set; }
}
