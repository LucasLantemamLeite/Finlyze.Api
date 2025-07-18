using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IFinancialTransactionRepository
{
    Task<int> CreateAsync(FinancialTransaction transaction);
    Task<int> DeleteAsync(FinancialTransaction transaction);
    Task<int> UpdateAsync(FinancialTransaction transaction);
}