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

        [Fact]
        public void ToEntity_WithPostValidParam_ReturnEntity()
        {
            //arrange
            CategoryPostDTO categoryPostDTO = new("Test");

            //act
            var category = categoryPostDTO.ToEntity();

            //assert
            Assert.NotNull(category);
            Assert.IsType<Category>(category);
            Assert.Equal(category.Name, categoryPostDTO.Name);
            Assert.Equal(default, category.Id);
        }

        [Fact]
        public void ToListDTO_WithValidParams_ReturnListOfDTOs()
        {
            //arrange
            List<Category> categories = [new("test1"), new("test2")];

            //act
            var listOfDtos = categories.ToListDTO();

            //assert
            Assert.NotNull(listOfDtos);
        }

        [Fact]
        public void CategoryPutDTOToEntity_WithValidParam_ReturnEntity()
        {
            //arrange
            CategoryPutDTO categoryDTO = new(11, "Test");

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