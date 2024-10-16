namespace Domain.Entities;

public class Chore : EntityModel
{
    public required int UserId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsCompleted { get; set; }
}
