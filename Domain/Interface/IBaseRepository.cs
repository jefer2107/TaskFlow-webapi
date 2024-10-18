namespace Domain.Interface;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> FindAll();
    Task<T> FindOne(int id);
    Task<T> Create(T model);
    Task<T> Update(int id, T model);
    Task<bool> Delete(int id);
}