namespace TodoList.DAL.Repository.Interface;

public interface IUnitOfWork : IDisposable
{
    ITodoRepository TodoRepository { get; }
    Task<int> SaveChangesAsync();
}
