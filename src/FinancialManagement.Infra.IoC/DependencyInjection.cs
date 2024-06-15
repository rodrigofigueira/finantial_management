using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FinancialManagement.Domain.Interfaces;
using FinancialManagement.Infra.Data;
using FinancialManagement.Application.Interfaces;
using FinancialManagement.Application.Services;
using FluentValidation;
using FinancialManagement.Application.Validators;

namespace FinancialManagement.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services
                                                           ,IConfiguration configuration)
        {
            services.AddScoped<SqlConnection>(provider =>
            {
                var connectionString = configuration.GetConnectionString("SQLServer");
                return new SqlConnection(connectionString);
            });
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

        public static IServiceCollection AddFluentValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
            return services;
        }
    }
}
