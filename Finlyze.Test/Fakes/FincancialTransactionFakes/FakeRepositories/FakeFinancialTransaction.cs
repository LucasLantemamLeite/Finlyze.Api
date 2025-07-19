using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;

namespace Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.FinancialTransactionFake;

public class FakeFinancialTransactionRepository : IFinancialTransactionRepository
{
    private readonly List<FinancialTransaction> _transactions = new();
    public FakeFinancialTransactionRepository(List<FinancialTransaction> transactions) => _transactions = transactions;

    public Task<int> CreateAsync(FinancialTransaction transaction)
    {
        _transactions.Add(transaction);
        return Task.FromResult(1);
    }

    public Task<int> DeleteAsync(FinancialTransaction transaction)
    {
        var existingTransaction = _transactions.FirstOrDefault(x => x.Id == transaction.Id);

        if (existingTransaction is null)
            return Task.FromResult(0);

        _transactions.Remove(transaction);
        return Task.FromResult(1);
    }

    public Task<int> UpdateAsync(FinancialTransaction transaction)
    {
        var index = _transactions.FindIndex(x => x.Id == transaction.Id);

        if (index == -1)
            return Task.FromResult(0);

        var financialTransaction = _transactions[index];

        financialTransaction.ChangeTitle(transaction.Title.Value);
        financialTransaction.ChangeDescription(transaction.Description.Value);
        financialTransaction.ChangeType((int)transaction.TranType.Value);
        financialTransaction.ChangeAmount(transaction.Amount.Value);
        financialTransaction.ChangeCreate(transaction.CreateAt.Value);
        financialTransaction.ChangeUpdate();

        _transactions[index] = financialTransaction;
        return Task.FromResult(1);
    }
}