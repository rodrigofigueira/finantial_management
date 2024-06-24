using FinancialManagement.Application.DTOs;
using FinancialManagement.Domain.Entities;

namespace FinancialManagement.Application.Mappings
{
    public static class CategoryMapping
    {
        public static Category ToEntity(this CategoryDTO categoryDTO)
        {
            return new(categoryDTO.Id, categoryDTO.Name);
        }

        public static CategoryDTO ToDTO(this Category category)
        {
            return new(category.Id, category.Name!);
        }

        public static Category ToEntity(this CategoryPostDTO categoryPostDTO)
        {
            return new(categoryPostDTO.Name);
        }

        public static IEnumerable<CategoryDTO> ToListDTO(this IEnumerable<Category> categories)
        {
            return categories.Select(x => x.ToDTO());
        }
    }
}
