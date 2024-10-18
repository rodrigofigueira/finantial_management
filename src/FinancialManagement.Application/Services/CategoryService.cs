namespace FinancialManagement.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<Result<CategoryDTO>> CreateAsync(CategoryPostDTO categoryPostDTO)
    {
        Category category = categoryPostDTO.ToEntity();
        var result = await categoryRepository.CreateAsync(category);

        if (result.IsFailure)
        {
            return Result<CategoryDTO>.Failure("Category creation failed");
        }

        CategoryDTO categoryToReturn = result.Value.ToDTO();
        return Result<CategoryDTO>.Success(categoryToReturn);
    }

    public async Task<Result<CategoryDTO>> GetByIdAsync(int id)
    {
        var result = await categoryRepository.GetByIdAsync(id);

        if (result.IsFailure)
        {
            return Result<CategoryDTO>.Failure("Category was not found");
        }

        CategoryDTO categoryDTO = result.Value.ToDTO();
        return Result<CategoryDTO>.Success(categoryDTO);
    }

    public async Task<Result<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
    {
        var result = await categoryRepository.GetCategoriesAsync();

        if (result.IsFailure)
        {
            return Result<IEnumerable<CategoryDTO>>.Failure("Category was not found");
        }

        var categoriesDTO = result.Value.ToListDTO();
        return Result<IEnumerable<CategoryDTO>>.Success(categoriesDTO);
    }

    public async Task<bool> RemoveAsync(int id) => await categoryRepository.RemoveAsync(id);

    public async Task<bool> UpdateAsync(CategoryPutDTO category)
    {
        var categoryEntity = category.ToEntity();
        return await categoryRepository.UpdateAsync(categoryEntity);
    }
}
