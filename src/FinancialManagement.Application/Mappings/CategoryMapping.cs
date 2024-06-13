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
    }
}
