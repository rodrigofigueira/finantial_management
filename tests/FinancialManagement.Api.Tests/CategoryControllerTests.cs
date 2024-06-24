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
        public async Task CreateAsync_WithValidParameters_ReturnOk()
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
        public async Task CreateAsync_WithInvalidParameters_ReturnBadRequest()
        {
            //arrange
            Mock<ICategoryService> categoryServiceMock = new();
            categoryServiceMock.Setup(c => c.CreateAsync(It.IsAny<CategoryPostDTO>()))
                .ReturnsAsync((CategoryPostDTO categoryPostDTO) =>
                    Result<CategoryDTO>.Success(new CategoryDTO(1, categoryPostDTO.Name)));

            List<ValidationFailure> validations = [new("Name", "Some error about name")];
            ValidationResult validationResult = new(validations);

            Mock<IValidator<CategoryPostDTO>> validatorMock = new();
            validatorMock.Setup(v => v.Validate(It.IsAny<CategoryPostDTO>())).Returns(validationResult);

            CategoryController categoryController = new(categoryServiceMock.Object);
            CategoryPostDTO categoryPostDTO = new("Test");

            //act
            var result = await categoryController.Create(categoryPostDTO, validatorMock.Object);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }


    }
}
