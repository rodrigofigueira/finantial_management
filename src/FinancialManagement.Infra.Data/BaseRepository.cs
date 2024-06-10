using Microsoft.Data.SqlClient;

namespace FinancialManagement.Infra.Data
{
    public class BaseRepository
    {
        protected readonly SqlConnection _connection;
        
        protected BaseRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }
    }
}
