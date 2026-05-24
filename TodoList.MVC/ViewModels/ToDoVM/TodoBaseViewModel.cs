namespace TodoList.MVC.ViewModels.ToDoVM;

public abstract class TodoBaseViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    [Display(Name = "Title")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Due Date")]
    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    [Required]
    [Display(Name = "Priority")]
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
}