namespace Application.DTOs.Chore;

public class ChoreInputCreateDTO
{
    public required int UserId { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; }
}
