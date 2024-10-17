namespace Domain.Interface;

public interface IUnityOfWork
{
    Task Commit();
}
