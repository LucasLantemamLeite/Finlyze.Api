using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFake;

namespace Finlyze.Test.Handlers.Fakes.UserAccountFakes;

[Trait("Category", "UserAccountHandler")]
public class FakeLoginUserAccountHandlerTest
{
    private List<UserAccount> _users;
    private FakeLoginUserAccountHandler _handler;

    public FakeLoginUserAccountHandlerTest()
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

        _handler = new FakeLoginUserAccountHandler(_users);
    }

    [Fact]
    public async Task Dado_Um_Email_E_Password_Válido_Faz_O_Login()
    {

        var command = new LoginUserAccountCommand("lucas.leite@finlyze.com", "SenhaForte123!");

        var result = await _handler.Handle(command);

        Assert.Equal("Login realizado com sucesso.", result.Message);
        Assert.NotNull(result.Data);
        Assert.True(_users.Any(x => x.Email.Value == command.Email));
    }

    [Fact]
    public async Task Dado_Um_Email_Inexistente_Não_Faz_O_Login()
    {
        var command = new LoginUserAccountCommand("EmailIncorreto@gmail.com", "SenhaForte123!");

        var result = await _handler.Handle(command);

        Assert.Equal("Credenciais Incorretas", result.Message);
        Assert.Null(result.Data);
        Assert.True(!_users.Any(x => x.Email.Value == command.Email));
    }

    [Fact]
    public async Task Dado_Um_Password_Incorreto_Não_Faz_O_Login()
    {
        var command = new LoginUserAccountCommand("EmailIncorreto@gmail.com", "SenhaNadaCerta123");

        var result = await _handler.Handle(command);

        Assert.Equal("Credenciais Incorretas", result.Message);
        Assert.Null(result.Data);
        Assert.True(!_users.Any(x => x.Password.Value == command.Password));
    }
}