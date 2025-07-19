using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Test.Abstract.Interfaces.Queries.Fakes.FinancialTransactionFake;

public class FakeFinancialTransactionQuery : IFinancialTransactionQuery
{
    private readonly List<FinancialTransaction> _transactions;

    public FakeFinancialTransactionQuery(List<FinancialTransaction> transactions) => _transactions = transactions;

    public Task<IEnumerable<FinancialTransaction>> GetByAmountAsync(decimal amount)
    {
        var transactions = _transactions.Where(t => t.Amount.Value == amount);
        return Task.FromResult(transactions);
    }

    public Task<IEnumerable<FinancialTransaction>> GetByCreateAtAsync(DateTime create_date)
    {
        var transactions = _transactions.Where(t => t.CreateAt.Value == create_date);
        return Task.FromResult(transactions);
    }

    public Task<FinancialTransaction?> GetByIdAsync(int id)
    {
        var transaction = _transactions.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(transaction);
    }

    public Task<IEnumerable<FinancialTransaction>> GetByTitleAsync(string title)
    {
        var transactions = _transactions.Where(t => t.Title.Value == title);
        return Task.FromResult(transactions);
    }

    public Task<IEnumerable<FinancialTransaction>> GetByTypeAsync(int type)
    {
        var transactions = _transactions.Where(t => t.TranType.Value == (EType)type);
        return Task.FromResult(transactions);
    }
}