namespace Domain.Entities;

public class Chore : EntityModel
{
    public int? UserId { get; set; }
    public int? CategoryId { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; }
    public bool? IsCompleted { get; set; }
    public User User { get; set; }
}
