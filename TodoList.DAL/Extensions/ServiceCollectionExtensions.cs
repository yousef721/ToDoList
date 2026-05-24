namespace TodoList.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        // GENERIC REPOSITORY
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // UNIT OF WORK
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
