using FinancialManagement.Api.Controllers;
using FinancialManagement.Application.DTOs;
using FinancialManagement.Application.Interfaces;
using FinancialManagement.Domain.Util;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinancialManagement.Api.Tests
{
    public class CategoryControllerTests
    {
        [Fact]
        public async Task Create_WithValidParameters_ReturnOk()
        {
            //arrange
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.CreateAsync(It.IsAny<CategoryPostDTO>()))
                .ReturnsAsync((CategoryPostDTO categoryPostDTO) => 
                    Result<CategoryDTO>.Success(new CategoryDTO(1, categoryPostDTO.Name)));

            Mock<IValidator<CategoryPostDTO>> validatorMock = new();
            validatorMock.Setup(v => v.Validate(It.IsAny<CategoryPostDTO>())).Returns(new FluentValidation.Results.ValidationResult());

            CategoryController categoryController = new(categoryServiceMock.Object);
            CategoryPostDTO categoryPostDTO = new("Test");

            //act
            var result = await categoryController.Create(categoryPostDTO, validatorMock.Object);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CategoryDTO>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);            
            Assert.NotNull(returnValue);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Test", returnValue.Name);
        }

        [Fact]
        public async Task Create_WithInvalidParameters_ReturnBadRequest()
        {
            //arrange
            List<ValidationFailure> validations = [new("Name", "Some error about name")];
            ValidationResult validationResult = new(validations);

            Mock<IValidator<CategoryPostDTO>> validatorMock = new();
            validatorMock.Setup(v => v.Validate(It.IsAny<CategoryPostDTO>())).Returns(validationResult);

            CategoryController categoryController = new(null!);            

            //act
            var result = await categoryController.Create(null!, validatorMock.Object);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task Delete_WithValidParameter_ReturnNoContent()
        {
            //arrange
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.RemoveAsync(It.IsAny<int>()))
                               .ReturnsAsync(true);

            CategoryController categoryController = new(categoryServiceMock.Object);

            //act
            var result = await categoryController.Delete(1);

            //assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task Delete_WithInvalidParameter_ReturnBadRequest()
        {
            //arrange
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.RemoveAsync(It.IsAny<int>()))
                               .ReturnsAsync(false);

            CategoryController categoryController = new(categoryServiceMock.Object);

            //act
            var result = await categoryController.Delete(1);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Category was not deleted", badRequestResult.Value);
        }

        [Fact]
        public async Task GetById_WithValidParameter_ReturnOk()
        {
            //arrange
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => Result<CategoryDTO>.Success(new CategoryDTO(1, "Test")));

            CategoryController categoryController = new(categoryServiceMock.Object);

            //act
            var result = await categoryController.GetById(1);

            //assert
            var okContentResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okContentResult.StatusCode);
        }

        [Fact]
        public async Task GetById_WithInvalidParameter_ReturnBadRequest()
        {
            //arrange
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => Result<CategoryDTO>.Failure("Category was not found"));

            CategoryController categoryController = new(categoryServiceMock.Object);

            //act
            var result = await categoryController.GetById(1);

            //assert
            var notFoundRequestContentResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundRequestContentResult.StatusCode);
            Assert.Equal("Category was not found", notFoundRequestContentResult.Value);
        }

        [Fact]
        public async Task Get_ReturnOk()
        {
            //arrange
            List<CategoryDTO> categories = [new(1,"Test1"), new(2, "Test2")];

            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.GetCategoriesAsync())
                .ReturnsAsync(() => Result<IEnumerable<CategoryDTO>>.Success(categories));

            CategoryController categoryController = new(categoryServiceMock.Object);

            //act
            var result = await categoryController.Get();

            //assert
            var okContentResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okContentResult.StatusCode);
            var categoriesFromEP = okContentResult.Value as List<CategoryDTO>;
            Assert.True(categoriesFromEP?.Any());
        }

        [Fact]
        public async Task Get_ReturnNotFound()
        {
            //arrange            
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.GetCategoriesAsync())
                .ReturnsAsync(() => Result<IEnumerable<CategoryDTO>>.Failure("Not found"));

            CategoryController categoryController = new(categoryServiceMock.Object);

            //act
            var result = await categoryController.Get();

            //assert
            var notFoundContentResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundContentResult.StatusCode);
            Assert.Equal("Not found", notFoundContentResult.Value);
        }
    }
}
