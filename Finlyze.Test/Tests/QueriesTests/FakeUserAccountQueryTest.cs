using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFake;

namespace Finlyze.Test.Queries.Fakes.UserAccountFake;

[Trait("Category", "UserAccountQuery")]
public class FakeUserAccountQueryTest
{
    private List<UserAccount> _users = new();
    private FakeUserAccountQuery _userQuery;

    public FakeUserAccountQueryTest()
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

        _userQuery = new FakeUserAccountQuery(_users);
    }

    [Fact]
    public async Task Dado_Um_Id_Existente_Trás_Entidade_Do_Banco()
    {
        var id = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var result = await _userQuery.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.True(_users.Any(x => x.Id == id));
    }

    [Fact]
    public async void Dado_Um_Id_Inexistente_Não_Trás_Do_Banco()
    {
        var id = Guid.Parse("22222222-2222-2222-2222-222222222223");

        var result = await _userQuery.GetByIdAsync(id);

        Assert.Null(result);
        Assert.True(!_users.Any(x => x.Id == id));
    }

    [Fact]
    public async void Dado_Um_Email_Existente_Trás_Do_Banco()
    {
        var email = "maria.silva@finlyze.com";

        var result = await _userQuery.GetByEmailAsync(email);

        Assert.NotNull(result);
        Assert.True(_users.Any(x => x.Email.Value == email));
    }

    [Fact]
    public async void Dado_Um_Email_Inexistente_Não_Trás_Do_Banco()
    {
        var email = "emailinexistente@gmail.com";

        var result = await _userQuery.GetByEmailAsync(email);

        Assert.Null(result);
        Assert.True(!_users.Any(x => x.Email.Value == email));
    }

    [Fact]
    public async void Dado_Um_PhoneNumber_Existente_Trás_Do_Banco()
    {
        var phone = "+5541999991234";

        var result = await _userQuery.GetByPhoneNumberAsync(phone);

        Assert.NotNull(result);
        Assert.True(_users.Any(x => x.PhoneNumber.Value == phone));
    }

    [Fact]
    public async void Dado_Um_PhoneNumber_Inexistente_Não_Trás_Do_Banco()
    {
        var phone = "+5562969445563";

        var result = await _userQuery.GetByPhoneNumberAsync(phone);

        Assert.Null(result);
        Assert.True(!_users.Any(x => x.PhoneNumber.Value == phone));
    }
}