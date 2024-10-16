using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext dbContext) 
: BaseRepository<User>(dbContext), IUserRepository
{
}
