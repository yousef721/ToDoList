namespace TodoList.MVC.ViewModels.ToDoVM;

public sealed class UpdateTodoViewModel
{
    [Required]
    public int Id { get; set; }

    [Display(Name = "Completed")]
    public bool IsCompleted { get; set; }
}
