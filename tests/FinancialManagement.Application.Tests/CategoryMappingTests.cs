using FinancialManagement.Application.DTOs;
using FinancialManagement.Application.Mappings;
using FinancialManagement.Domain.Entities;

namespace FinancialManagement.Application.Tests
{
    public class CategoryMappingTests
    {
        [Fact]
        public void ToDTO_WithValidParam_ReturnDTO()
        {
            //arrange
            Category category = new(11, "Test");

            //act
            var categoryDTO = category.ToDTO();

            //assert
            Assert.NotNull(categoryDTO);
            Assert.IsType<CategoryDTO>(categoryDTO);
            Assert.Equal(category.Name, categoryDTO.Name);
            Assert.Equal(category.Id, categoryDTO.Id);
        }

        [Fact]
        public void ToEntity_WithValidParam_ReturnEntity()
        {
            //arrange
            CategoryDTO categoryDTO = new(11, "Test");

            //act
            var category = categoryDTO.ToEntity();

            //assert
            Assert.NotNull(category);
            Assert.IsType<Category>(category);
            Assert.Equal(category.Name, categoryDTO.Name);
            Assert.Equal(category.Id, categoryDTO.Id);
        }
    }
}