namespace FinancialManagement.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetProductsAsync();
    Task<Transaction> GetByIdAsync(int id);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<Transaction> UpdateAsync(Transaction transaction);
    Task<Transaction> RemoveAsync(int id);
}
