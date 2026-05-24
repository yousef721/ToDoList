namespace TodoList.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IAuthServices, AuthService>();
        services.AddAutoMapper(x => x.AddProfile(new TodoMappingProfile()));
        return services;
    }
}