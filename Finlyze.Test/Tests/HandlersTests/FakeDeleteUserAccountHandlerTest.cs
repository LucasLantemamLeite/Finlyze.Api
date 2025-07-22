using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFake;

namespace Finlyze.Test.Handlers.Fakes.UserAccountFake;

[Trait("Category", "UserAccountHandler")]
public class FakeDeleteUserAccountTest
{
    private List<UserAccount> _users = new();
    public FakeDeleteUserAccountHandler _handler;

    public FakeDeleteUserAccountTest()
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

        _handler = new FakeDeleteUserAccountHandler(_users);
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Válido_Remove_Do_Banco()
    {
        var userAccount = new UserAccount(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "Maria Silva",
                "maria.silva@finlyze.com",
                "SeguraEssaSenha456@",
                "+5511988884321",
                DateOnly.FromDateTime(new DateTime(1999, 12, 25)),
                DateTime.UtcNow.AddDays(-3),
                true,
                1);

        var command = new DeleteUserAccountCommand(userAccount.Id);

        var result = await _handler.Handle(command);

        Assert.Equal("Conta do usuário deletado com sucesso.", result.Message);
        Assert.NotNull(result.Data);
        Assert.True(!_users.Any(x => x.Id == userAccount.Id));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Inexistente_Não_Remove_Do_Banco()
    {
        var userAccount = new UserAccount(
               Guid.Parse("22222222-2222-2222-2222-222222222223"),
               "Maria Silva",
               "maria.silva@finlyze.com",
               "SeguraEssaSenha456@",
               "+5511988884321",
               DateOnly.FromDateTime(new DateTime(1999, 12, 25)),
               DateTime.UtcNow.AddDays(-3),
               true,
               1);

        var command = new DeleteUserAccountCommand(userAccount.Id);

        var result = await _handler.Handle(command);

        Assert.Equal("Conta do usuário com esse Id não encontrado.", result.Message);
        Assert.Null(result.Data);
        Assert.True(!_users.Any(x => x.Id == userAccount.Id));
    }
}