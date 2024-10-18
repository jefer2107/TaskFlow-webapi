namespace Application.DTOs.Category;

public class CategoryInputUpdateDTO : EntityModelDTO
{
    public int? UserId { get; set; }
    public string? Name { get; set; }
}
