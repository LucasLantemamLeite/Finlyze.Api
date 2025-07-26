using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Repositories;

public interface IFinancialTransactionRepository
{
    Task<int> CreateAsync(FinancialTransaction transaction);
    Task<int> DeleteAsync(FinancialTransaction transaction);
    Task<int> UpdateAsync(FinancialTransaction transaction);
}