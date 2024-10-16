using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class BaseRepository<T>(AppDbContext dbContext) 
: IBaseRepository<T> where T : EntityModel
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IEnumerable<T>> FindAll()
    {
        try
        {
            IEnumerable<T> models = await _dbContext
            .Set<T>().ToListAsync();
            
            return models;
        }
        catch (Exception error)
        {
            throw new Exception($"Error in method FindAll {typeof(T)}, error: {error}");
        }
    }

    public Task<T> FindOne(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> Create(T model)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update(int id, T model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

}
