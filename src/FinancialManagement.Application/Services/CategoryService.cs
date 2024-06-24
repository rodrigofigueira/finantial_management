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

        public async Task<Result<CategoryDTO>> GetByIdAsync(int id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);

            if (result.IsFailure)
            {
                return Result<CategoryDTO>.Failure("Category was not found");
            }

            CategoryDTO categoryDTO = result.Value.ToDTO();
            return Result<CategoryDTO>.Success(categoryDTO);
        }

        public async Task<Result<IEnumerable<CategoryDTO>>> GetCategoriesAsync()
        {
            var result = await _categoryRepository.GetCategoriesAsync();

            if (result.IsFailure)
            {
                return Result<IEnumerable<CategoryDTO>>.Failure("Category was not found");
            }

            var categoriesDTO = result.Value.ToListDTO();
            return Result<IEnumerable<CategoryDTO>>.Success(categoriesDTO);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var removed = await _categoryRepository.RemoveAsync(id);
            return removed;
        }

        public async Task<bool> UpdateAsync(CategoryDTO category)
        {
            var categoryEntity = category.ToEntity();
            var wasUpdated = await _categoryRepository.UpdateAsync(categoryEntity);
            return wasUpdated;
        }
    }
}
