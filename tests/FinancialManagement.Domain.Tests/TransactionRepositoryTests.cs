namespace FinancialManagement.Domain.Tests;

public class TransactionRepositoryTests
{
    [Fact]
    public void CreateTransaction_WithValidParameters_ResultObjectValidState()
    {
        //arrange 
        const string NAME = "Name to test";
        const decimal VALUE = 99.9m;
        const string categoryName = "Category";
        Category category = new(categoryName);

        //act
        Transaction transaction = new(NAME, VALUE, category);

        //assert
        Assert.NotNull(transaction);
        Assert.Equal(NAME, transaction.Name);
        Assert.Equal(VALUE, transaction.Value);
        Assert.Equal(category.Name, transaction.Category?.Name);
    }

    [Fact]
    public void CreateTransaction_WithNegativeId_ThrowsException()
    {
        //arrange 
        const int ID = -1;
        const string NAME = "Name to test";
        const decimal VALUE = 99.9m;
        const string categoryName = "Category";
        Category category = new(categoryName);

        //act and assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => new Transaction(ID, NAME, VALUE, category));
        Assert.Equal("Invalid Id value", exception.Message);
    }

    [Fact]
    public void CreateTransaction_WithNullName_ThrowsException()
    {
        //arrange 
        const int ID = 1;
        const string? NAME = null;
        const decimal VALUE = 99.9m;
        const string categoryName = "Category";
        Category category = new(categoryName);

        //act and assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => new Transaction(ID, NAME, VALUE, category));
        Assert.Equal("Invalid name, Name is required", exception.Message);
    }

    [Fact]
    public void CreateTransaction_WithShortName_ThrowsException()
    {
        //arrange 
        const int ID = 1;
        const string NAME = "ab";
        const decimal VALUE = 99.9m;
        const string categoryName = "Category";
        Category category = new(categoryName);

        //act and assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => new Transaction(ID, NAME, VALUE, category));
        Assert.Equal("Invalid name, too short, minimum 3 characters", exception.Message);
    }

    [Fact]
    public void CreateTransaction_WithTooLongName_ThrowsException()
    {
        //arrange 
        const int ID = 1;
        const string NAME = "abcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyzabcdefghijklmnopqrstuvxyz";
        const decimal VALUE = 99.9m;
        const string categoryName = "Category";
        Category category = new(categoryName);

        //act and assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => new Transaction(ID, NAME, VALUE, category));
        Assert.Equal("Invalid name, too long, maximum 200 characters", exception.Message);
    }

    [Fact]
    public void CreateTransaction_WithInvalidValue_ThrowsException()
    {
        //arrange 
        const int ID = 1;
        const string NAME = "Transaction Name";
        const decimal VALUE = -1m;
        const string categoryName = "Category";
        Category category = new(categoryName);

        //act and assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => new Transaction(ID, NAME, VALUE, category));
        Assert.Equal("Invalid transaction value", exception.Message);
    }

    [Fact]
    public void CreateTransaction_WithNullCategory_ThrowsException()
    {
        //arrange 
        const int ID = 1;
        const string NAME = "Transaction Name";
        const decimal VALUE = 1m;
        const Category? category = null;

        //act and assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => new Transaction(ID, NAME, VALUE, category));
        Assert.Equal("Category is required", exception.Message);
    }

    [Fact]
    public void UpdateTransaction_WithValidParameters_ResultObjectValidState()
    {
        //arrange 
        const string NAME = "Name to test";
        const decimal VALUE = 99.9m;
        const string CATEGORY_NAME = "Category";
        Category CATEGORY = new(CATEGORY_NAME);
        Transaction transaction = new(NAME, VALUE, CATEGORY);

        const string NAME_CHANGED = "Name to test";
        const decimal VALUE_CHANGED = 99.9m;
        const string CATEGORY_NAME_CHANGED = "Category";
        Category CATEGORY_CHANGED = new(CATEGORY_NAME_CHANGED);

        //act
        transaction.Update(NAME_CHANGED, VALUE_CHANGED, CATEGORY_CHANGED);

        //assert
        Assert.NotNull(transaction);
        Assert.Equal(NAME_CHANGED, transaction.Name);
        Assert.Equal(VALUE_CHANGED, transaction.Value);
        Assert.Equal(CATEGORY_NAME_CHANGED, transaction.Category?.Name);
    }

}
