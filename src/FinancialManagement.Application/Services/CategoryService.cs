using FinancialManagement.Application.DTOs;
using FinancialManagement.Application.Interfaces;
using FinancialManagement.Application.Mappings;
using FinancialManagement.Domain.Entities;
using FinancialManagement.Domain.Interfaces;
using FinancialManagement.Domain.Util;

namespace FinancialManagement.Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<Result<CategoryDTO>> CreateAsync(CategoryPostDTO categoryPostDTO)
        {
            Category category = categoryPostDTO.ToEntity();
            var result = await _categoryRepository.CreateAsync(category);

            if (result.IsFailure)
            {
                return Result<CategoryDTO>.Failure("Category creation failed");
            }

            CategoryDTO categoryToReturn = result.Value.ToDTO();
            return Result<CategoryDTO>.Success(categoryToReturn);
        }

        public Task<Result<CategoryDTO>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
