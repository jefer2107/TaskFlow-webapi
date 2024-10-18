namespace Application.DTOs.Chore;

public class ChoreInputUpdateDTO
{
    public int? UserId { get; set; }
    public int? CategoryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}
