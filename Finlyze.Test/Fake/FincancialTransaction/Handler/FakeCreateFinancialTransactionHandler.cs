using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.FinancialTransactionEntity;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.FinancialTransactionFakes;

namespace Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.FinancialTransactionFake;

public class FakeCreateFinancialTransactionHandler : ICreateFinancialTransactionHandler
{
    private List<FinancialTransaction> _transactions = new();
    private FakeFinancialTransactionRepository _tranRepository;

    public FakeCreateFinancialTransactionHandler(List<FinancialTransaction> transactions)
    {
        _transactions = transactions;
        _tranRepository = new FakeFinancialTransactionRepository(_transactions);
    }

    public async Task<ResultHandler<FinancialTransaction>> Handle(CreateFinancialTransactionCommand command)
    {
        var transaction = new FinancialTransaction(command.Title, command.Description, command.Amount, command.TranType, command.CreateAt, command.UserAccountId);

        var rows = await _tranRepository.CreateAsync(transaction: transaction);

        if (rows == 0)
            return ResultHandler<FinancialTransaction>.Fail("Erro ao criar transação.");

        return ResultHandler<FinancialTransaction>.Ok("Transação criada com sucesso.", transaction);

    }
}