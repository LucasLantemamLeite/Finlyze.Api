using System.Transactions;

namespace Finlyze.Application.Abstract.Interface;

public interface ITransactionQuery
{
    Task<Transaction> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetByTitleAsync(string title);
    Task<IEnumerable<Transaction>> GetByTypeAsync(int type);
    Task<IEnumerable<Transaction>> GetByCreateAtAsync(DateTime create_date);
    Task<IEnumerable<Transaction>> GetByAmountAsync(decimal amount);
}