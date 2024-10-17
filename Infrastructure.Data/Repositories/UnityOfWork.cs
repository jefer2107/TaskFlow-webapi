using Domain.Interface;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class UnityOfWork(AppDbContext context) : IUnityOfWork
{
    private readonly AppDbContext _context = context;
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

}
