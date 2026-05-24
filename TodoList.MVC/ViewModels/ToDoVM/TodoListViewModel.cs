namespace TodoList.MVC.ViewModels.ToDoVM;

public sealed class TodoListViewModel
{
    public PagedResult<TodoListItemViewModel> PagedResult { get; set; } = new();
    public string? SearchTerm { get; set; }
    public string? Filter { get; set; }
    public int TotalAll { get; set; }
    public int TotalCompleted { get; set; }
    public int TotalPending { get; set; }
}
