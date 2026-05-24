
namespace TodoList.BLL.Services.Implementation;

public class TodoService : ITodoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TodoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<TodoDto>> GetPagedAsync(TodoQueryParameters parameters, string? filter)
    {
        var (items, totalCount) = await _unitOfWork.TodoRepository.GetPagedAsync(
            parameters.UserId,
            parameters.SearchTerm,
            filter,
            parameters.PageNumber,
            parameters.PageSize);

        return new PagedResult<TodoDto>
        {
            Items = _mapper.Map<IEnumerable<TodoDto>>(items),
            TotalCount = totalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<TodoDto?> GetByIdAsync(int id, string userId)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(id);

        if (todo == null || todo.UserId != userId)
            return null;

        return _mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto> CreateAsync(CreateTodoDto dto)
    {
        var todo = _mapper.Map<TodoItem>(dto);

        await _unitOfWork.TodoRepository.AddAsync(todo);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto> UpdateAsync(UpdateTodoDto dto)
    {
        var existing = await _unitOfWork.TodoRepository.GetByIdAsync(dto.Id);

        if (existing == null || existing.UserId != dto.UserId)
            throw new KeyNotFoundException($"Todo {dto.Id} not found.");

        _mapper.Map(dto, existing);

        _unitOfWork.TodoRepository.Update(existing);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TodoDto>(existing);
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(id);

        if (todo == null || todo.UserId != userId)
            throw new KeyNotFoundException($"Todo {id} not found.");

        _unitOfWork.TodoRepository.Delete(todo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ToggleCompletedAsync(int id, string userId)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(id);

        if (todo == null || todo.UserId != userId)
            throw new KeyNotFoundException($"Todo {id} not found.");

        todo.IsCompleted = !todo.IsCompleted;

        _unitOfWork.TodoRepository.Update(todo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> CountByFilterAsync(string userId, string? filter)
    {
        return await _unitOfWork.TodoRepository.CountAsync(x =>
            x.UserId == userId &&
            (filter == null ||
            filter == "all" ||
            (filter == "completed" && x.IsCompleted) ||
            (filter == "pending" && !x.IsCompleted))
        );
    }
}