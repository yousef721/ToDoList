namespace TodoList.BLL.DTOs.ToDoDto;

public sealed class CreateTodoDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    [Required]
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public string UserId { get; set; } = string.Empty;
}