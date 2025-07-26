using Finlyze.Domain.Entities.FinancialTransactionEntity;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.FinancialTransactionFake;

namespace Finlyze.Test.Repositories.Fakes.FinancialTransactionFakes;

[Trait("Category", "FinancialTransactionRepository")]
public class FakeFinancialTransactionRepositoryTest
{
    private List<FinancialTransaction> _transactions;
    private FakeFinancialTransactionRepository _tranRepository;

    public FakeFinancialTransactionRepositoryTest()
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
                new DateTime(2024, 08, 06),
                new DateTime(2024, 08, 07),
                Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
            )
        };

        _tranRepository = new FakeFinancialTransactionRepository(_transactions);
    }

    [Fact]
    public async Task Dado_Um_FinancialTransaction_Válido_Adiciona_No_Banco()
    {
        var financialTransaction = new FinancialTransaction(
            3,
            "Padaria",
            "Compra pão",
            6.75m,
            2,
            new DateTime(2022, 06, 05),
            new DateTime(2022, 07, 01),
            Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
        );

        var result = await _tranRepository.CreateAsync(financialTransaction);

        Assert.Equal(1, result);
        Assert.True(_transactions.Any(x => x.Id == financialTransaction.Id));
    }

    [Fact]
    public async Task Dado_Um_FinancialTransaction_Válido_Remove_do_Banco()
    {
        var financialTransaction = new FinancialTransaction(
            2,
            "Supermercado",
            "Compra mensal no mercado",
            820.75m,
            2,
            new DateTime(2024, 08, 06),
            new DateTime(2024, 08, 07),
            Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
        );

        var result = await _tranRepository.DeleteAsync(financialTransaction);

        Assert.Equal(1, result);
        Assert.True(!_transactions.Any(x => x.Id == financialTransaction.Id));
    }

    [Fact]
    public async Task Dado_Um_FinancialTransaction_Inexistente_Não_Remove_do_Banco()
    {
        var financialTransaction = new FinancialTransaction(
            3,
            "Padaria",
            "Compra pão",
            6.75m,
            2,
            new DateTime(2022, 06, 05),
            new DateTime(2022, 07, 01),
            Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
        );

        var result = await _tranRepository.DeleteAsync(financialTransaction);

        Assert.Equal(0, result);
        Assert.True(!_transactions.Any(x => x.Id == financialTransaction.Id));
    }

    [Fact]
    public async Task Dado_Um_FinancialTransaction_Válido_Atualiza_No_Banco()
    {
        var financialTransaction = new FinancialTransaction(
            2,
            "Padaria",
            "Compra pão",
            6.75m,
            2,
            new DateTime(2022, 06, 05),
            new DateTime(2022, 07, 01),
            Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
        );

        var result = await _tranRepository.UpdateAsync(financialTransaction);

        Assert.Equal(1, result);
        Assert.True(_transactions.Any(x => x.Id == financialTransaction.Id));
    }

    [Fact]
    public async Task Dado_Um_FinancialTransaction_Inexistente_Atualiza_No_Banco()
    {
        var financialTransaction = new FinancialTransaction(
            3,
            "Padaria",
            "Compra pão",
            6.75m,
            2,
            new DateTime(2022, 06, 05),
            new DateTime(2022, 07, 01),
            Guid.Parse("c1d2e3f4-5a67-89b0-c1d2-e3f4567890ab")
        );

        var result = await _tranRepository.UpdateAsync(financialTransaction);

        Assert.Equal(0, result);
        Assert.True(!_transactions.Any(x => x.Id == financialTransaction.Id));
    }
}