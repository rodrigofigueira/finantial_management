using FinancialManagement.Domain.Validations;

namespace FinancialManagement.Domain.Entities
{
    public sealed class Transaction : EntityBase
    {
        public string? Name { get; private set; }
        public decimal Value { get; private set; }
        public Category? Category { get; private set; }

        public Transaction(string name, decimal value, Category category)
        {
            ValidateDomain(name, value, category);
        }

        public Transaction(int id, string? name, decimal value, Category? category)
        {
            DomainExceptionValidation.When(id < 0,
                                        "Invalid Id value");

            ValidateDomain(name, value, category);
            Id = id;
        }

        public void Update(string name, decimal value, Category category)
        {
            ValidateDomain(name, value, category);
        }

        private void ValidateDomain(string? name, decimal value, Category? category)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                                           "Invalid name, Name is required");

            DomainExceptionValidation.When(name?.Length < 3,
                                           "Invalid name, too short, minimum 3 characters");

            DomainExceptionValidation.When(name?.Length > 200,
                                           "Invalid name, too long, maximum 200 characters");

            DomainExceptionValidation.When(value < 0,
                               "Invalid transaction value");

            DomainExceptionValidation.When(category is null,
                               "Category is required");

            Name = name;
            Value = value;
            Category = category;
        }
    }
}
