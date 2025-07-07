using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface ITransactionRepository
{
    Task<int?> CreateAsync(Transaction transaction);
    Task<int?> DeleteAsync(Transaction transaction);
    Task<int?> UpdateAsync(Transaction transaction);
}