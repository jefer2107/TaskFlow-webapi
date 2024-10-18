using Domain.Entities;

namespace Domain.Interface;

public interface ICategoryRepository 
: IBaseRepository<Category>
{
    Task<Category>? FindOneByUser(int id, int userId);
}
