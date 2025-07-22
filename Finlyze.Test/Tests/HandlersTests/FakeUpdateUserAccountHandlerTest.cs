using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFake;

namespace Finlyze.Test.Handlers.Fakes.UserAccountFake;

[Trait("Category", "UserAccountHandler")]
public class FakeUpdateUserAccountHandlerTest
{
    private List<UserAccount> _users = new();
    public FakeUpdateUserAccountHandler _handler;

    public FakeUpdateUserAccountHandlerTest()
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

        _handler = new FakeUpdateUserAccountHandler(_users);
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Válido_Atualiza_No_Banco()
    {
        var userAccount = new UserAccount(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "Maria Silva",
                "novoemail@gmail.com",
                "SenhaNova1323",
                "+5599995160777",
                DateOnly.FromDateTime(new DateTime(2005, 05, 20)),
                DateTime.UtcNow.AddDays(-5),
                true,
                1);

        var command = new UpdateUserAccountCommand(userAccount.Id, "SeguraEssaSenha456@", userAccount.Name.Value, userAccount.Email.Value, userAccount.Password.Value, userAccount.PhoneNumber.Value, userAccount.BirthDate.Value);

        var result = await _handler.Handle(command);

        Assert.Equal("Conta do usuário atualizado com sucesso.", result.Message);
        Assert.NotNull(result.Data);
        Assert.True(_users.Any(x => x.Email.Value == userAccount.Email.Value));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_Confirm_Password_Incorreta_Não_Atualiza_No_Banco()
    {
        var userAccount = new UserAccount(
                 Guid.Parse("22222222-2222-2222-2222-222222222222"),
                 "Maria Silva",
                 "novoemail@gmail.com",
                 "SenhaNova1323",
                 "+5599995160777",
                 DateOnly.FromDateTime(new DateTime(2005, 05, 20)),
                 DateTime.UtcNow.AddDays(-5),
                 true,
                 1);

        var command = new UpdateUserAccountCommand(userAccount.Id, "SenhaErrada", userAccount.Name.Value, userAccount.Email.Value, userAccount.Password.Value, userAccount.PhoneNumber.Value, userAccount.BirthDate.Value);

        var result = await _handler.Handle(command);

        Assert.Equal("Senha incorreta.", result.Message);
        Assert.Null(result.Data);
        Assert.True(!_users.Any(x => x.Email.Value == userAccount.Email.Value));
    }
}