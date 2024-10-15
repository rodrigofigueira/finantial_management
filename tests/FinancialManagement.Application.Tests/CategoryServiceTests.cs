namespace FinancialManagement.Application.Tests;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateAsync_WithValidValues_ReturnDTO()
    {
        //arrange
        Mock<ICategoryRepository> _repository = new();
        _repository.Setup(r => r.CreateAsync(It.IsAny<Category>()))
                                .ReturnsAsync((Category category)
                                                => Result<Category>.Success(new(1, category.Name!)));

        CategoryService categoryService = new(_repository.Object);
        CategoryPostDTO categoryPostDTO = new("Mock");

        //act
        var result = await categoryService.CreateAsync(categoryPostDTO);

        //assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.NotNull(result.Value);
        Assert.Equal(categoryPostDTO.Name, result.Value.Name);
        Assert.Equal(1, result.Value.Id);
    }

}
