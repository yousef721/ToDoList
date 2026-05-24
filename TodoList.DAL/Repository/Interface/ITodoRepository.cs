namespace TodoList.DAL.Repository.Interface;

public interface ITodoRepository : IGenericRepository<TodoItem>
{
    Task<(IEnumerable<TodoItem> Items, int TotalCount)> GetPagedAsync(  
        string userId,
        string? searchTerm,
        string? filter,
        int pageNumber,
        int pageSize);
}
