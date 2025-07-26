using Finlyze.Domain.ValueObjects.UserAccountObjects;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Test.Validation.Entities;

[Trait("Category", "UserAccount")]
public class UserAccountTest
{
    [Theory]
    [InlineData("Lucas")]
    [InlineData("Ana")]
    [InlineData("Bruno")]
    public static void Dado_Um_Nome_Válido_Adiciona_Na_Entidade(string name)
    {
        var ex = Record.Exception(() => new Name(name));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("         ")]
    [InlineData(null)]
    public static void Dado_Um_Nome_Vazio_Ou_Nulo_Lança_Exceção(string name)
    {
        var ex = Assert.Throws<DomainException>(() => new Name(name));
        Assert.NotNull(ex);
        Assert.Equal("Name não pode ser vazio ou nulo.", ex.Message);
    }

    [Theory]
    [InlineData("teste@gmail.com")]
    [InlineData("teste@gmail.com.br")]
    [InlineData("TESTE@GMAIL.COM")]
    [InlineData("teste1234@gmail.com")]
    [InlineData("teste1234-_.%@gmail.com")]
    public static void Dado_Um_Email_Válido_Adiciona_Na_Entidade(string email)
    {
        var ex = Record.Exception(() => new Email(email));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("     ")]
    public static void Dado_Um_Email_Vazio_Lança_Exceção(string email)
    {
        var ex = Assert.Throws<EmailRegexException>(() => new Email(email));
        Assert.NotNull(ex);
        Assert.Equal("Email Inválido.", ex.Message);
    }

    [Theory]
    [InlineData("Teste12343")]
    [InlineData("TESTE")]
    [InlineData("teste")]
    public static void Dado_Um_Email_Sem_Domínio_Lança_Exceção(string email)
    {
        var ex = Assert.Throws<EmailRegexException>(() => new Email(email));
        Assert.NotNull(ex);
        Assert.Equal("Email Inválido.", ex.Message);
    }

    [Theory]
    [InlineData("@gmail.com")]
    [InlineData("@gmail.com.br")]
    [InlineData("@GMAIL.COM.BR")]
    public static void Dado_Um_Email_Sem_Parte_Local_Lança_Exceção(string email)
    {
        var ex = Assert.Throws<EmailRegexException>(() => new Email(email));
        Assert.NotNull(ex);
        Assert.Equal("Email Inválido.", ex.Message);
    }

    [Theory]
    [InlineData("Senha")]
    [InlineData("Senha123")]
    [InlineData("Senha-.1@145")]
    public static void Dado_Um_Password_Válido_Adiciona_Na_Entidade(string password)
    {
        var ex = Record.Exception(() => new Password(password));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("         ")]
    [InlineData(null)]
    public static void Dado_Um_Password_Vazio_Ou_Nulo_Lança_Exceção(string password)
    {
        var ex = Assert.Throws<DomainException>(() => new Password(password));
        Assert.NotNull(ex);
        Assert.Equal("Password não pode ser vazio ou nulo.", ex.Message);
    }

    [Theory]
    [InlineData("+5544995344918")]
    [InlineData("+5598997275533")]
    [InlineData("+5589996551213")]
    public static void Dado_Um_PhoneNumber_Válido_Adiciona_Na_Entidade(string phone)
    {
        var ex = Record.Exception(() => new PhoneNumber(phone));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData("5544995344918")]
    [InlineData("5598997275533")]
    [InlineData("5589996551213")]
    public static void Dado_Um_PhoneNumber_Sem_Mais_Lança_Exceção(string phone)
    {
        var ex = Assert.Throws<PhoneNumberRegexException>(() => new PhoneNumber(phone));
        Assert.NotNull(ex);
        Assert.Equal("PhoneNumber Inválido.", ex.Message);
    }

    [Theory]
    [InlineData("+")]
    [InlineData("")]
    public static void Dado_Um_PhoneNumber_Sem_Digítos_Lança_Exceção(string phone)
    {
        var ex = Assert.Throws<PhoneNumberRegexException>(() => new PhoneNumber(phone));
        Assert.NotNull(ex);
        Assert.Equal("PhoneNumber Inválido.", ex.Message);
    }

    [Theory]
    [InlineData("+5544995344918564")]
    [InlineData("+55989972755337891")]
    [InlineData("+558999655121336971")]
    public static void Dado_Um_PhoneNumber_Com_Mais_De_15_Digítos_Lança_Exceção(string phone)
    {
        var ex = Assert.Throws<PhoneNumberRegexException>(() => new PhoneNumber(phone));
        Assert.NotNull(ex);
        Assert.Equal("PhoneNumber Inválido.", ex.Message);
    }

    [Theory]
    [InlineData("+5544995")]
    [InlineData("+559899")]
    [InlineData("+55896")]
    public static void Dado_Um_PhoneNumber_Com_Menos_De_8_Digítos_Lança_Exceção(string phone)
    {
        var ex = Assert.Throws<PhoneNumberRegexException>(() => new PhoneNumber(phone));
        Assert.NotNull(ex);
        Assert.Equal("PhoneNumber Inválido.", ex.Message);
    }

    [Theory]
    [InlineData(2000, 01, 01)]
    [InlineData(1990, 01, 01)]
    [InlineData(1980, 01, 01)]
    public static void Dado_Um_BirthDate_Válido_Adiciona_Na_Entidade(int ano, int mes, int dia)
    {
        var date = DateOnly.FromDateTime(new DateTime(ano, mes, dia));
        var ex = Record.Exception(() => new BirthDate(date));
        Assert.Null(ex);
    }

    [Fact]
    public static void Dado_Um_BirthDate_Futuro_Lança_Exceção()
    {
        var date = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        var ex = Assert.Throws<BirthDateException>(() => new BirthDate(date));
        Assert.NotNull(ex);
        Assert.Equal("Data não pode ser futura.", ex.Message);
    }

    [Fact]
    public static void Dado_Um_BirthDate_Menor_De_Idade_Lança_Exceção()
    {
        var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-17));
        var ex = Assert.Throws<BirthDateException>(() => new BirthDate(date));
        Assert.NotNull(ex);
        Assert.Equal("Usuário não pode ser menor de idade.", ex.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public static void Dado_Um_Role_Válido_Adiciona_Na_Entidade(int role)
    {
        var ex = Record.Exception(() => new Role(role));
        Assert.Null(ex);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(-1)]
    public static void Dado_Um_Role_Inválido_Lança_Exceção(int role)
    {
        var ex = Assert.Throws<EnumFlagsException>(() => new Role(role));
        Assert.NotNull(ex);
        Assert.Equal("Role Inválido.", ex.Message);
    }
}