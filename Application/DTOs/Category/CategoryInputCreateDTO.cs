namespace Application.DTOs.Category;

public class CategoryInputCreateDTO : EntityModelDTO
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
}
