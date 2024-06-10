using FinancialManagement.Domain.Entities;
using FinancialManagement.Domain.Validations;

namespace FinancialManagement.Domain.Tests
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            //arrange 
            string NAME = "Name to test";

            //act
            Category category = new(NAME);

            //assert
            Assert.NotNull(category);
            Assert.Equal(NAME, category.Name);
        }

        [Fact]
        public void CreateCategory_WithNegativeId_ThrowsException()
        {
            //arrange 
            string NAME = "Name to test";
            int ID = -1;

            //act and assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(ID, NAME));
            Assert.Equal("Invalid Id value", exception.Message);
        }

        [Fact]
        public void CreateCategory_WithShortName_ThrowsException()
        {
            //arrange 
            string NAME = "a";
            int ID = 1;

            //act and assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(ID, NAME));
            Assert.Equal("Invalid name, too short, minimum 3 characters", exception.Message);
        }

        [Fact]
        public void CreateCategory_WithNullName_ThrowsException()
        {
            //arrange 
            string? NAME = null;
            int ID = 1;

            //act and assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(ID, NAME!));
            Assert.Equal("Invalid name, Name is required", exception.Message);
        }

        [Fact]
        public void UpdateCategory_WithValidParameters_ResultObjectValidState()
        {
            //arrange 
            string NAME = "Name to test";
            string NEW_NAME = "Name Changed";
            Category category = new(NAME);

            //act
            category.Update(NEW_NAME);

            //assert
            Assert.NotNull(category);
            Assert.Equal(NEW_NAME, category.Name);
        }

    }
}