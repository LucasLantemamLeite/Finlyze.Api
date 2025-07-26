using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFakes;

namespace Finlyze.Test.Handlers.Fakes.UserAccountFakes;

[Trait("Category", "UserAccountHandler")]
public class FakeCreateUserAccountHandlerTest
{
    private List<UserAccount> _users = new();

    private FakeCreateUserAccountHandler _handler;

    public FakeCreateUserAccountHandlerTest()
    {
        _users = new List<UserAccount>
        {
            new UserAccount(
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "Lucas Leite",
                "lucas.leite@finlyze.com",
                "SenhaForte123!",
                "+5541999991234",
                DateOnly.FromDateTime(new DateTime(2005, 04, 08)),
                DateTime.UtcNow,
                true,
                1
            ),
            new UserAccount(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "Maria Silva",
                "maria.silva@finlyze.com",
                "SeguraEssaSenha456@",
                "+5511988884321",
                DateOnly.FromDateTime(new DateTime(1999, 12, 25)),
                DateTime.UtcNow.AddDays(-3),
                true,
                1
            )
        };

        _handler = new FakeCreateUserAccountHandler(_users);
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Válido_Adiciona_Na_Entidade()
    {
        var command = new CreateUserAccountCommand("Lucas", "lucas.leite2@finlyze.com.br", "senha1234", "+5591998916388", DateOnly.FromDateTime(new DateTime(2005, 04, 08)));

        var result = await _handler.Handle(command);

        Assert.Equal("Conta do usuário criado com sucesso.", result.Message);
        Assert.NotNull(result.Data);
        Assert.True(_users.Any(x => x.Email.Value == command.Email));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_Email_Já_Em_Uso_Não_Adiciona_Na_Entidade()
    {
        var command = new CreateUserAccountCommand("Lucas", "maria.silva@finlyze.com", "senha1234", "+5591998916388", DateOnly.FromDateTime(new DateTime(2005, 04, 08)));

        var result = await _handler.Handle(command);

        Assert.Equal("Email já está sendo usado.", result.Message);
        Assert.Null(result.Data);
        Assert.True(_users.Any(x => x.Email.Value == command.Email));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_PhoneNumber_Já_Em_Uso_Não_Adiciona_Na_Entidade()
    {
        var command = new CreateUserAccountCommand("Lucas", "Testedeemail@gmail.com", "senha1234", "+5541999991234", DateOnly.FromDateTime(new DateTime(2005, 04, 08)));

        var result = await _handler.Handle(command);

        Assert.Equal("Número de telefone já está sendo usado.", result.Message);
        Assert.Null(result.Data);
        Assert.True(_users.Any(x => x.PhoneNumber.Value == command.PhoneNumber));
    }
}