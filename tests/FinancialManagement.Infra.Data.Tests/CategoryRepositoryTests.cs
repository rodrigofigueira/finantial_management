using FinancialManagement.Domain.Entities;
using FinancialManagement.Infra.Data.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinancialManagement.Infra.Data.Tests
{
    public class CategoryRepositoryTests
    {
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;
        private readonly SqlConnection con;
        private readonly CategoryRepository categoryRepository;

        public CategoryRepositoryTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
                                        .SetBasePath(AppContext.BaseDirectory)
                                        .AddJsonFile("appsettings.json", 
                                                    optional: false, 
                                                    reloadOnChange: true);

            _configuration = configurationBuilder.Build();
            _connectionString = _configuration.GetConnectionString("SQLServer");
            con = new(_connectionString);
            categoryRepository = new(con);
            MigrationRunner.RunMigrations(_connectionString);
        }

        [Fact]
        public async void GetByIdAsync_WithValidValue_ReturnObject()
        {
            //arrange
            Category category = new(GenerateRandomName());
            var resultCreate = await categoryRepository.CreateAsync(category);

            //act
            var resultGet = await categoryRepository.GetByIdAsync(resultCreate.Value.Id);

            //assert
            Assert.NotNull(resultGet);
            Assert.Equal(resultGet.Value.Id, resultCreate.Value.Id);

            //clean
            await categoryRepository.RemoveAsync(resultCreate.Value.Id);
        }

        [Fact]
        public async void CreateAsync_WithValidValue_ReturnObject()
        {
            //arrange
            Category category = new(GenerateRandomName());

            //act
            var resultCreate = await categoryRepository.CreateAsync(category);

            //assert
            Assert.NotNull(resultCreate);
            Assert.True(resultCreate.Value.Id > 0);

            //clean
            await categoryRepository.RemoveAsync(resultCreate.Value.Id);
        }

        [Fact]
        public async void RemoveAsync_WithValidValue_ReturnObject()
        {
            //arrange
            Category category = new(GenerateRandomName());
            var resultCreate = await categoryRepository.CreateAsync(category);

            //act
            bool itWasDeleted = await categoryRepository.RemoveAsync(resultCreate.Value.Id);

            //assert
            Assert.NotNull(resultCreate);
            Assert.True(resultCreate.Value.Id > 0);
            Assert.True(itWasDeleted);
        }

        [Fact]
        public async void UpdateAsync_WithValidValue_ReturnObject()
        {
            //arrange
            Category category = new(GenerateRandomName());
            var resultCreate = await categoryRepository.CreateAsync(category);
            Category categoryToUpdate = new(resultCreate.Value.Id
                                            ,GenerateRandomName());

            //act
            bool itWasUpdated = await categoryRepository.UpdateAsync(categoryToUpdate);
            var resultGet = await categoryRepository.GetByIdAsync(resultCreate.Value.Id);

            //assert            
            Assert.True(itWasUpdated);
            Assert.Equal(resultGet.Value.Name, categoryToUpdate.Name);

            //clean
            await categoryRepository.RemoveAsync(resultCreate.Value.Id);
        }

        [Fact]
        public async void GetCategoriesAsync_WithValidValue_ReturnObject()
        {
            //arrange
            List<Category> categories = new();

            for (int i = 0; i < 10; i++)
            {
                var category = new Category(GenerateRandomName());
                var result = await categoryRepository.CreateAsync(category);
                categories.Add(result.Value);
            }

            //act
            var categoriesFromDB = await categoryRepository.GetCategoriesAsync();

            //assert            
            Assert.True(categoriesFromDB.Value.Count() >= 10);

            //clean
            foreach (var _category in categories)
            {
                await categoryRepository.RemoveAsync(_category.Id);
            }
        }

        private static string GenerateRandomName()
        {
            return $"Test {DateTime.Now.ToString("mm:ss:fffff")}";
        }
    }
}
