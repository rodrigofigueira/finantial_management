using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FinancialManagement.Infra.IoC;

namespace FinancialManagement.Infra.Data.Tests
{
    public abstract class BaseTests
    {
        public IConfiguration Configuration { get; private set; }
        public string ConnectionString { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }

        public BaseTests()
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile("appsettings.json")
                            .Build();

            ConnectionString = Configuration.GetConnectionString("SQLServer")
                                        ?? throw new ArgumentException("Connection String was not found!");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddInfrastructure(Configuration);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

    }
}
