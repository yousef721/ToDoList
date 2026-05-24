namespace TodoList.DAL.Repository.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ITodoRepository TodoRepository {get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {

        _context = context;
        TodoRepository = new TodoRepository(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
