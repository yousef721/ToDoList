namespace TodoList.DAL.Repository.Implementation;

public class TodoRepository : GenericRepository<TodoItem>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context) { }

     public async Task<(IEnumerable<TodoItem> Items, int TotalCount)> GetPagedAsync(
        string userId,
        string? searchTerm,
        string? filter,
        int pageNumber,
        int pageSize)
    {
        var query = _dbSet.Where(t => t.UserId == userId);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim().ToLower();
            query = query.Where(t =>
                t.Title.ToLower().Contains(term) ||
                (t.Description != null && t.Description.ToLower().Contains(term)));
        }

        query = filter?.ToLower() switch
        {
            "completed" => query.Where(t => t.IsCompleted),
            "pending"   => query.Where(t => !t.IsCompleted),
            _           => query
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return (items, totalCount);
    }
}
