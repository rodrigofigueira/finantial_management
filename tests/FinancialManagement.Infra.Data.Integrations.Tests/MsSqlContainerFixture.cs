namespace FinancialManagement.Infra.Data.Integrations.Tests;

public class MsSqlContainerFixture : IAsyncLifetime
{
    private MsSqlContainer Testcontainer { get; set; } = null!;
    private SqlConnection SqlConnection { get; set; } = null!;
    public string ConnectionString { get; private set; } = string.Empty;

    public async Task DisposeAsync()
    {
        await TruncateTables();
        await SqlConnection.DisposeAsync();
        await Testcontainer.StopAsync();
    }

    private async Task TruncateTables()
    {
        string truncateTable = await File.ReadAllTextAsync("Scripts/TruncateTables.sql"); ;
        using var command = new SqlCommand(truncateTable, SqlConnection);
        await command.ExecuteNonQueryAsync();
    }

    public async Task InitializeAsync()
    {
        Testcontainer = new MsSqlBuilder()
                    .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                    .Build();

        await Testcontainer.StartAsync();
        ConnectionString = Testcontainer.GetConnectionString();
        await CreateDatabaseAndTables(ConnectionString);
    }

    private async Task CreateDatabaseAndTables(string connectionString)
    {
        SqlConnection = new SqlConnection(connectionString);
        await SqlConnection.OpenAsync();

        var createTableScript = await File.ReadAllTextAsync("Scripts/CreateTables.sql");
        using var command = new SqlCommand(createTableScript, SqlConnection);
        await command.ExecuteNonQueryAsync();
    }

}
