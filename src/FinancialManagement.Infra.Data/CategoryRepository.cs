using Dapper;
using Microsoft.Data.SqlClient;
using FinancialManagement.Domain.Entities;
using FinancialManagement.Domain.Interfaces;
using FinancialManagement.Domain.Util;

namespace FinancialManagement.Infra.Data
{
    public class CategoryRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
                                                                   ,ICategoryRepository
    {

        public async Task<Result<Category>> CreateAsync(Category category)
        {
            var sql = @"INSERT INTO categories (name) 
                        output Inserted.Id
                        values (@name)";
            
            var param = new { name = category.Name };

            var id = await _connection.ExecuteScalarAsync<int>(sql, param);

            if (id > 0 && category.Name is not null)
            {
                return Result<Category>.Success(new Category(id, category.Name));
            }

            return Result<Category>.Failure("Error at creating");

        }

        public async Task<Result<Category>> GetByIdAsync(int id)
        {
            var sql = @"select Id, Name from categories where id = @id";
            var param = new { id };
            var category = await _connection.QuerySingleOrDefaultAsync<Category>(sql, param);

            if (category != null) {
                return Result<Category>.Success(category);
            }
            
            return Result<Category>.Failure("Not found");
        }

        public async Task<Result<IEnumerable<Category>>> GetCategoriesAsync()
        {
            string sql = "select Id, Name from categories";
            var categories = await _connection.QueryAsync<Category>(sql, null);

            if (categories is null)
                return Result<IEnumerable<Category>>.Failure("Not found");

            return Result<IEnumerable<Category>>.Success(categories);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var sql = @"delete from categories where id = @id";
            var param = new { id };
            int rowsDeleted = await _connection.ExecuteAsync(sql, param);
            return rowsDeleted == 1;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            var sql = @"update categories set name = @name where id = @id";
            int rowsUpdated = await _connection.ExecuteAsync(sql, category);
            return rowsUpdated == 1;
        }
    }
}
