using Domain.Entities;

namespace Domain.Interface;

public interface IUserRepository
: IBaseRepository<User>
{
    Task<User?> FindByEmail(string email);
}
