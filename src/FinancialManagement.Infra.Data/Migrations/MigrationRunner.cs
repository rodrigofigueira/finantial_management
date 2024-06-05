using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManagement.Infra.Data.Migrations
{
    public static class MigrationRunner
    {
        public static void RunMigrations(string? connectionString)
        {
            ArgumentNullException.ThrowIfNull(connectionString, "Connection String is null");

            var serviceProvider = CreateServices(connectionString);
            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        private static ServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(MigrationRunner).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        public static void RevertMigrations(string? connectionString)
        {
            ArgumentNullException.ThrowIfNull(connectionString, "Connection String is null");
            var serviceProvider = CreateServices(connectionString);
            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateDown(0);
        }
    }
}
