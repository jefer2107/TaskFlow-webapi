namespace Application.DTOs.Chore;

public class ChoreDTO : EntityModelDTO
{
    public required int UserId { get; set; }
    public int? CategoryId { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
