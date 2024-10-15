namespace FinancialManagement.Application.Tests.Validators;

public class PutCategoryValidatorTests
{
    public IServiceProvider ServiceProvider { get; private set; }

    public PutCategoryValidatorTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFluentValidator();
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public void PutCategory_WithInvalidName_ReturnError()
    {
        //arrange
        var validator = ServiceProvider.GetRequiredService<IValidator<CategoryPutDTO>>();
        CategoryPutDTO categoryPutDTO = new(1, "Te");

        //act
        var result = validator.Validate(categoryPutDTO);

        //assert
        Assert.False(result.IsValid);
    }
}
