using FinancialManagement.Application.DTOs;
using FinancialManagement.Infra.IoC;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialManagement.Application.Tests.Validators
{
    public class CreateCategoryValidatorTests
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public CreateCategoryValidatorTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddFluentValidator();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void CreateCategory_WithInvalidName_ReturnError()
        {
            //arrange
            var validator = ServiceProvider.GetRequiredService<IValidator<CategoryPostDTO>>();
            CategoryPostDTO categoryPostDTO = new("1");

            //act
            var result = validator.Validate(categoryPostDTO);

            //assert
            Assert.False(result.IsValid);
        }
    }
}
