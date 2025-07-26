using Finlyze.Domain.Entities.FinancialTransactionEntity;
using Finlyze.Domain.ValueObjects.Enums;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.FinancialTransactionFake;

namespace Finlyze.Test.Queries.Fakes.FinancialTransactionFakes;

[Trait("Category", "FinancialTransactionQuery")]
public class FakeFinancialTransactionQueryTest
{
    private List<FinancialTransaction> _transactions = new();
    private FakeFinancialTransactionQuery _tranQuery;

    public FakeFinancialTransactionQueryTest()
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

        _tranQuery = new FakeFinancialTransactionQuery(_transactions);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Dado_Um_Id_Existente_Trás_Do_Banco(int id)
    {
        var transaction = await _tranQuery.GetByIdAsync(id);

        Assert.NotNull(transaction);
        Assert.True(_transactions.Any(x => x.Id == id));
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(99)]
    public async Task Dado_Um_Id_Inexistente_Não_Trás_Do_Banco(int id)
    {
        var transaction = await _tranQuery.GetByIdAsync(id);

        Assert.Null(transaction);
        Assert.True(!_transactions.Any(x => x.Id == id));
    }

    [Theory]
    [InlineData("Salário")]
    [InlineData("Supermercado")]
    public async Task Dado_Um_Title_Existente_Trás_Do_Banco(string title)
    {
        var result = await _tranQuery.GetByTitleAsync(title);

        Assert.NotEmpty(result);
        Assert.True(_transactions.Any(x => x.Title.Value == title));
    }

    [Theory]
    [InlineData("Compras")]
    [InlineData("Salão")]
    [InlineData("Padária")]
    public async Task Dado_Um_Title_Inexistente_Não_Trás_Do_Banco(string title)
    {
        var result = await _tranQuery.GetByTitleAsync(title);

        Assert.Empty(result);
        Assert.True(!_transactions.Any(x => x.Title.Value == title));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Dado_Um_TranType_Existente_Trás_Do_Banco(int type)
    {
        var result = await _tranQuery.GetByTypeAsync(type);

        Assert.NotEmpty(result);
        Assert.True(_transactions.Any(x => x.TranType.Value == (EType)type));
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public async Task Dado_Um_TranType_Inexistente_Não_Trás_Do_Banco(int type)
    {
        var result = await _tranQuery.GetByTypeAsync(type);

        Assert.Empty(result);
        Assert.True(!_transactions.Any(x => x.TranType.Value == (EType)type));
    }

    [Theory]
    [InlineData(4500.00)]
    [InlineData(820.75)]
    public async Task Dado_Um_Amount_Existente_Trás_Do_Banco(decimal amount)
    {
        var result = await _tranQuery.GetByAmountAsync(amount);

        Assert.NotEmpty(result);
        Assert.True(_transactions.Any(x => x.Amount.Value == amount));
    }

    [Theory]
    [InlineData(2300.12)]
    [InlineData(156.35)]
    [InlineData(99999.35)]
    public async Task Dado_Um_Amount_Inexistente_Não_Trás_Do_Banco(decimal amount)
    {
        var result = await _tranQuery.GetByAmountAsync(amount);

        Assert.Empty(result);
        Assert.True(!_transactions.Any(x => x.Amount.Value == amount));
    }

    [Theory]
    [InlineData(2025, 03, 26)]
    [InlineData(2024, 08, 06)]
    public async Task Dado_Um_CreateAt_Existente_Trás_Do_Banco(int ano, int mes, int dia)
    {
        var date = new DateTime(ano, mes, dia);

        var result = await _tranQuery.GetByCreateAtAsync(date);

        Assert.NotEmpty(result);
        Assert.True(_transactions.Any(x => x.CreateAt.Value == date));
    }

    [Theory]
    [InlineData(2002, 12, 25)]
    [InlineData(2023, 05, 01)]
    [InlineData(2020, 07, 09)]
    public async Task Dado_Um_CreateAt_Inexistente_Trás_Do_Banco(int ano, int mes, int dia)
    {
        var date = new DateTime(ano, mes, dia);

        var result = await _tranQuery.GetByCreateAtAsync(date);

        Assert.Empty(result);
        Assert.True(!_transactions.Any(x => x.CreateAt.Value == date));
    }
}