using Application.DTOs.Category;

namespace Application.Interfaces;

public interface ICategoryService 
: IBaseService<CategoryDTO, CategoryInputCreateDTO, CategoryInputUpdateDTO>
{
    Task<IEnumerable<CategoryDTO>> FindAllByUser(int userId);
    Task<CategoryDTO> FindOneByUser(int id, int userId);
    Task<CategoryDTO> UpdateByUser(int id, int userId, CategoryInputUpdateDTO model);
    Task<bool> DeleteByUser(int id, int userId);
}
