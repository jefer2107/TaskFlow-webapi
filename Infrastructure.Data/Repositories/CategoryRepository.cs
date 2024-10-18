using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class CategoryRepository(AppDbContext context)
: BaseRepository<Category>(context), ICategoryRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Category>? FindOneByUser(int id, int userId)
    {
        try
        {
            return await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }
        catch (Exception error)
        {
            
            Console.WriteLine($"Error in method FindOneByUser Category");
            throw new Exception(error.Message);
        }
    }

}
