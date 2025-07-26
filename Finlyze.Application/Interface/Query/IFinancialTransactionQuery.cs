using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Queries;

public interface IFinancialTransactionQuery
{
    Task<FinancialTransaction?> GetByIdAsync(int id);
    Task<IEnumerable<FinancialTransaction>> GetByTitleAsync(string title);
    Task<IEnumerable<FinancialTransaction>> GetByTypeAsync(int type);
    Task<IEnumerable<FinancialTransaction>> GetByCreateAtAsync(DateTime create_date);
    Task<IEnumerable<FinancialTransaction>> GetByAmountAsync(decimal amount);
}