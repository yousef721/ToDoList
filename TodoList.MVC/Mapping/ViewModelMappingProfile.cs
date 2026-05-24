namespace TodoList.MVC.Mapping;

public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        // Auth
        CreateMap<RegisterViewModel, RegisterDto>();

        CreateMap<LoginViewModel, LoginDto>();


        // Create Todo
        CreateMap<CreateTodoViewModel, CreateTodoDto>();


        // Edit Todo
        CreateMap<EditTodoViewModel, UpdateTodoDto>();

        CreateMap<TodoDto, EditTodoViewModel>();


        // Todo List / Details
        CreateMap<TodoDto, TodoListItemViewModel>();
    }
}