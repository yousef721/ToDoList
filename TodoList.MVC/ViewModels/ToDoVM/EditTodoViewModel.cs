namespace TodoList.MVC.ViewModels.ToDoVM;

public sealed class EditTodoViewModel : TodoBaseViewModel
{
    public int Id { get; set; }

    public bool IsCompleted { get; set; }
}