using Finlyze.Domain.ValueObject.TransactionObjects;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Test.Validation.Transaction;

[Trait("Category", "Transaction")]
public class TransactionTest
{

    [Theory]
    [InlineData("Salário Junho")]
    [InlineData("Presente Aniversário")]
    [InlineData("Supermercado")]
    public static void Dado_Um_Title_Válido_Adiciona_Na_Entidade(string title)
    {
        var ex = Record.Exception(() => new TransactionTitle(title));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public static void Dado_Um_Title_Vazio_Ou_Nulo_Lança_Exceção(string title)
    {
        var ex = Assert.Throws<DomainException>(() => new TransactionTitle(title));
        Assert.NotNull(ex);
        Assert.Equal("Title não pode ser vazio ou nulo.", ex.Message);
    }

    [Theory]
    [InlineData(500)]
    [InlineData(300)]
    [InlineData(-400)]
    public static void Dado_Um_Amount_Válido_Adiciona_Na_Entidade(decimal amount)
    {
        var ex = Record.Exception(() => new Amount(amount));
        Assert.Null(ex);
    }

    [Fact]
    public static void Dado_Um_Amount_Igual_A_Zero_Lança_Exceção()
    {
        var ex = Assert.Throws<DomainException>(() => new Amount(0));
        Assert.NotNull(ex);
        Assert.Equal("Amount não pode ser igual a 0.", ex.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public static void Dado_Um_TypeTransaction_Válido_Adiciona_Na_Entidade(int type)
    {
        var ex = Record.Exception(() => new TypeTransaction(type));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(999)]
    [InlineData(-1)]
    public static void Dado_Um_TypeTransaction_Inválido_Lança_Exceção(int type)
    {
        var ex = Assert.Throws<EnumException>(() => new TypeTransaction(type));
        Assert.NotNull(ex);
        Assert.Equal("Type Inválido.", ex.Message);
    }
}