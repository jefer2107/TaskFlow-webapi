using Application.DTOs.Chore;

namespace Application.DTOs.Category;

public class CategoryDTO : EntityModelDTO
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
    public ICollection<ChoreDTO> Chores { get; set; }

}
