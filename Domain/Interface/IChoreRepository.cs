using Domain.Entities;

namespace Domain.Interface;

public interface IChoreRepository 
: IBaseRepository<Chore>
{
    Task<Chore> FindOneByUser(int id, int userId);
}
