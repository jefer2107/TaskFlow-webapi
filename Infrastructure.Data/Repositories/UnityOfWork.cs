using Domain.Interface;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class UnityOfWork(AppDbContext context) : IUnityOfWork
{
    private readonly AppDbContext _context = context;
    public Task Commit()
    {
        await _context.SaveChangesAsync();
    }

}
