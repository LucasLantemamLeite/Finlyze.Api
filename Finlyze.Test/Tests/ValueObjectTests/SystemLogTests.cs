using Finlyze.Domain.ValueObject.AppLogObjects;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Test.Validation.Entities;

[Trait("Category", "SystemLog")]
public class SystemLogTest
{

    [Theory]
    [InlineData("Conta Criada")]
    [InlineData("Transcation Criada")]
    [InlineData("Conta Delete")]
    public static void Dado_Um_Title_Válido_Adiciona_Na_Entidade(string title)
    {
        var ex = Record.Exception(() => new Title(title));
        Assert.Null(ex);
    }


    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData(null)]
    public static void Dado_Um_Title_Vazio_Ou_Nulo_Lança_Exceção(string title)
    {
        var ex = Assert.Throws<DomainException>(() => new Title(title));
        Assert.NotNull(ex);
        Assert.Equal("Title não pode ser vazio ou nulo.", ex.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public static void Dado_Um_LogType_Válido_Adiciona_Na_Entidade(int type)
    {
        var ex = Record.Exception(() => new LogType(type));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(999)]
    [InlineData(-1)]
    public static void Dado_Um_LogType_Inválido_Lança_Exceção(int type)
    {
        var ex = Assert.Throws<EnumException>(() => new LogType(type));
        Assert.NotNull(ex);
        Assert.Equal("Enum Inválido.", ex.Message);
    }


}