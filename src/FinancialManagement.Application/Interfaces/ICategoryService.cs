namespace FinancialManagement.Application.Interfaces;

public interface ICategoryService
{
    Task<Result<IEnumerable<CategoryDTO>>> GetCategoriesAsync();
    Task<Result<CategoryDTO>> GetByIdAsync(int id);
    Task<Result<CategoryDTO>> CreateAsync(CategoryPostDTO categoryDTO);
    Task<bool> UpdateAsync(CategoryPutDTO categoryDTO);
    Task<bool> RemoveAsync(int id);
}
