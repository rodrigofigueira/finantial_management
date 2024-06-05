using FinancialManagement.Infra.Data.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinancialManagement.Infra.Data.Tests
{
    public class CreateTableTests
    {
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;

        public CreateTableTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = configurationBuilder.Build();
            _connectionString = _configuration.GetConnectionString("SQLServer");
        }

        [Theory]
        [InlineData("categories")]
        [InlineData("transactions")]
        public void RunMigrations_CreateTables(string tableName)
        {

            //arrange and act            
            MigrationRunner.RunMigrations(_connectionString);
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand($"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'", connection);
            var result = command.ExecuteScalar();

            //assert
            Assert.NotNull(result);

            //clean
            MigrationRunner.RevertMigrations(_connectionString);
        }
    }
}
