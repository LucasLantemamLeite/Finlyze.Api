using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.FinancialTransactionFake;

namespace Finlyze.Test.Handlers.Fakes.FinancialTransactionFake;

[Trait("Category", "FinancialTransactionHandler")]
public class FakeCreateFinancialTransactionHandlerTest
{
    private List<FinancialTransaction> _transactions = new();
    private FakeCreateFinancialTransactionHandler _handler;

    public FakeCreateFinancialTransactionHandlerTest()
    {
        _transactions = new List<FinancialTransaction>
                {
                    new FinancialTransaction(
                1,
                "Salário",
                "Recebimento do mês de julho",
                4500.00m,
                1,
                new DateTime(2025, 03, 26),
                new DateTime(2025, 07, 04),
                Guid.Parse("a3b1f2e6-9f34-4d2b-9f8f-12ab34cd56ef")
            ),
            new FinancialTransaction(
                2,
                "Supermercado",
                "Compra mensal no mercado",
                820.75m,
                2,
                new DateTime(2024,08,06),
                new DateTime(2024,08,07),
                Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
            )
                };

        _handler = new FakeCreateFinancialTransactionHandler(_transactions);
    }

    [Fact]
    public async Task Dado_Um_FinancialTransaction_Válido_Adiciona_No_Banco()
    {
        var transaction = new FinancialTransaction(2, "Comprara pão", null, 500.36m, 2, new DateTime(2022, 08, 05), new DateTime(2022, 08, 25), Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab"));

        var command = new CreateFinancialTransactionCommand(transaction.Title.Value, transaction.Description?.Value, transaction.Amount.Value, (int)transaction.TranType.Value, transaction.CreateAt.Value, transaction.UserAccountId);

        var result = await _handler.Handle(command);

        Assert.Equal("Transação criada com sucesso.", result.Message);
        Assert.True(_transactions.Any(x => x.Id == transaction.Id));
    }
}