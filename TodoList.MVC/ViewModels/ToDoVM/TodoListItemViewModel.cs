namespace TodoList.MVC.ViewModels.ToDoVM;

public sealed class TodoListItemViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }

    public TodoPriority Priority { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public bool IsOverdue =>
        DueDate.HasValue &&
        DueDate.Value.Date < DateTime.Today &&
        !IsCompleted;

    public string ShortDescription =>
        string.IsNullOrWhiteSpace(Description)
            ? ""
            : Description.Length > 60
                ? Description[..60] + "..."
                : Description;

    public string PriorityBadgeClass => Priority switch
    {
        TodoPriority.High => "badge bg-danger",
        TodoPriority.Medium => "badge bg-warning text-dark",
        TodoPriority.Low => "badge bg-success",
        _ => "badge bg-secondary"
    };
}