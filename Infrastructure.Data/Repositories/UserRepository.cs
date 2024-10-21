using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext dbContext)

: BaseRepository<User>(dbContext), IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<User?> FindByEmail(string email)
    {
        try
        {
            User user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }
        catch (Exception error)
        {
            
            throw new Exception(
                $"Error user repository in method FindByEmail: {error}"
            );
        }
    }

    public async Task<User?> FindOneWithChores(int id)
    {
        try
        {
            User user = await _dbContext.Users
            .Include(u => u.Chores)
            .FirstOrDefaultAsync(e => e.Id == id);

            return user;
        }
        catch (Exception error)
        {
            
            throw new Exception(
                $"Error user repository in method FindOneWithChores: {error}"
            );
        }
    }
}
