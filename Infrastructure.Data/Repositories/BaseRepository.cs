using Domain;
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

    public async Task<T> FindOne(int id)
    {
        try
        {
            T model = await _dbContext.Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id);

            return model;
        }
        catch (Exception error)
        {
            
            Console.WriteLine($"Error in method FindOne {typeof(T)}");
            throw new Exception(error.ToString());
        }
    }

    public async Task<T> Create(T model)
    {
        try
        {
            var modelCreated = await _dbContext.Set<T>()
            .AddAsync(model);

            return modelCreated.Entity;
        }
        catch (Exception error)
        {
            
            Console.WriteLine($"Error in method Create {typeof(T)}: {error}");
            throw new Exception(error.Message);
        }
    }

    public async Task<T> Update(int id, T model)
    {
        try
        {
            var modelById = await FindOne(id);

            if(modelById == null)
            {
                throw new Exception($"ID:{id} não encontrado");
            }
            else
            {
                
                CopyProperties<T>.Set(model, modelById);
                
                 _dbContext.Set<T>().Entry(modelById).State = EntityState.Modified;

                return modelById;
            }
        }
        catch (Exception error)
        {
            
            Console.WriteLine($"Error in method Update {typeof(T)}");
            throw new Exception(error.Message);
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var modelById = await FindOne(id);

            if(modelById == null)
            {
                throw new Exception($"ID:{id} não encontrado");
            }
            else
            {
                var modelDeleted = _dbContext.Set<T>()
                .Remove(modelById);

                if(modelDeleted == null) return false;
                else return true;
            }
        }
        catch (Exception error)
        {
            
            Console.WriteLine($"Error in method Delete {typeof(T)}");
            throw new Exception(error.Message);
        }
    }

}
