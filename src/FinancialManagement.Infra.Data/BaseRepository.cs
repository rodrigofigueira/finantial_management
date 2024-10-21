namespace FinancialManagement.Infra.Data;

public class BaseRepository
{
    protected readonly SqlConnection _connection;

    protected BaseRepository(SqlConnection sqlConnection) => _connection = sqlConnection;

    public async Task<bool> CreateDatabase(string databaseName)
    {
        string create = @$"
                            IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = @databaseName)
                            BEGIN
                              CREATE DATABASE @databaseName;
                            END;
                            GO

                          ";

        var param = new { databaseName };

        var databaseWasCreated = await _connection.ExecuteAsync(create, param);

        if (databaseWasCreated > 0)
            return true;

        return false;
    }

}
