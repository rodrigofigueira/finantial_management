namespace FinancialManagement.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddScoped(provider =>
        {
            var connectionString = configuration.GetConnectionString("SQLServer");
            return new SqlConnection(connectionString);
        });
        _ = services.AddScoped<ICategoryRepository, CategoryRepository>();
        _ = services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }

    public static IServiceCollection AddFluentValidator(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
}
