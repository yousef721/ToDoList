namespace TodoList.BLL.Mapping;

public sealed class TodoMappingProfile : Profile
{
    public TodoMappingProfile()
    {
        CreateMap<TodoItem, TodoDto>();

        CreateMap<CreateTodoDto, TodoItem>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<UpdateTodoDto, TodoItem>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted,   opt => opt.Ignore())
            .ForMember(dest => dest.UserId,      opt => opt.MapFrom(src => src.UserId));
    }
}