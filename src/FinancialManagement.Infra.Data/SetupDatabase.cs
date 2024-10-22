namespace FinancialManagement.Infra.Data;

public static class SetupDatabase
{
    public static async Task Execute(string connectionString)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var createTableScript = await File.ReadAllTextAsync("Scripts/CreateTables.sql");
        using var command = new SqlCommand(createTableScript, connection);
        await command.ExecuteNonQueryAsync();
    }

}
