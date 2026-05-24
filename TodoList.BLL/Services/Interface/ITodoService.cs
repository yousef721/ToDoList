namespace TodoList.BLL.Services.Interface;

public interface ITodoService
{
    Task<PagedResult<TodoDto>> GetPagedAsync(TodoQueryParameters parameters, string? filter);
    Task<TodoDto?> GetByIdAsync(int id, string userId);
    Task<TodoDto> CreateAsync(CreateTodoDto dto);
    Task<TodoDto> UpdateAsync(UpdateTodoDto dto);
    Task DeleteAsync(int id, string userId);
    Task ToggleCompletedAsync(int id, string userId);
    Task<int> CountByFilterAsync(string userId, string? filter);
}
