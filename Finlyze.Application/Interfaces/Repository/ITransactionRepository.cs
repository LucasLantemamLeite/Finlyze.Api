
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface ITransactionRepository
{
    Task<ResultPattern<Transaction>> CreateAsync(Transaction transaction);
    Task<ResultPattern<Transaction>> DeleteAsync(Transaction transaction);
    Task<ResultPattern<Transaction>> UpdateAsync(Transaction transaction);

}