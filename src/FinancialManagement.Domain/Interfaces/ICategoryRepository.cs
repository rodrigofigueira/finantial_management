namespace FinancialManagement.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Result<IEnumerable<Category>>> GetCategoriesAsync();
    Task<Result<Category>> GetByIdAsync(int id);
    Task<Result<Category>> CreateAsync(Category category);
    Task<bool> UpdateAsync(Category category);
    Task<bool> RemoveAsync(int id);
}
