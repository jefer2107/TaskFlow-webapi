using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ChoreRepository(AppDbContext context)
: BaseRepository<Chore>(context), IChoreRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Chore> FindOneByUser(int id, int userId)
    {
        try
        {
            return await _context.Chores
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
        catch (Exception error)
        {
            
            Console.WriteLine($"Error in method FindOneByUser Chore");
            throw new Exception(error.Message);
        }
    }

}
