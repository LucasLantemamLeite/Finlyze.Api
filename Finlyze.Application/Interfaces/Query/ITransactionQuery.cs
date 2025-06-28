using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;

namespace Finlyze.Application.Abstract.Interface;

public interface ITransactionQuery
{
    Task<ResultPattern<TransactionDto>> GetByIdAsync(Guid id);
    Task<ResultPattern<IEnumerable<TransactionDto>>> GetByTitleAsync(string title);
    Task<ResultPattern<IEnumerable<TransactionDto>>> GetByTypeAsync(int type);
    Task<ResultPattern<IEnumerable<TransactionDto>>> GetByCreateAtAsync(DateTime create_date);
    Task<ResultPattern<IEnumerable<TransactionDto>>> GetByAmountAsync(decimal amount);
}