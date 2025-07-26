using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFakes;

namespace Finlyze.Test.Queries.Fakes.UserAccountFakes;

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

    [Theory]
    [InlineData("11111111-1111-1111-1111-111111111111")]
    [InlineData("22222222-2222-2222-2222-222222222222")]
    public async Task Dado_Um_Id_Existente_Trás_Entidade_Do_Banco(Guid id)
    {
        var result = await _userQuery.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.True(_users.Any(x => x.Id == id));
    }

    [Theory]
    [InlineData("33333333-3333-3333-3333-333333333333")]
    [InlineData("44444444-4444-4444-4444-444444444444")]
    [InlineData("55555555-5555-5555-5555-555555555555")]
    [InlineData("99999999-9999-9999-9999-999999999999")]
    public async void Dado_Um_Id_Inexistente_Não_Trás_Do_Banco(Guid id)
    {
        var result = await _userQuery.GetByIdAsync(id);

        Assert.Null(result);
        Assert.True(!_users.Any(x => x.Id == id));
    }

    [Theory]
    [InlineData("maria.silva@finlyze.com")]
    [InlineData("lucas.leite@finlyze.com")]
    public async void Dado_Um_Email_Existente_Trás_Do_Banco(string email)
    {
        var result = await _userQuery.GetByEmailAsync(email);

        Assert.NotNull(result);
        Assert.True(_users.Any(x => x.Email.Value == email));
    }

    [Theory]
    [InlineData("emailinexistente1@gmail.com")]
    [InlineData("fulanocabramacho@gmail.com")]
    [InlineData("nãoexisteesseemail@gmail.com")]
    public async void Dado_Um_Email_Inexistente_Não_Trás_Do_Banco(string email)
    {
        var result = await _userQuery.GetByEmailAsync(email);

        Assert.Null(result);
        Assert.True(!_users.Any(x => x.Email.Value == email));
    }

    [Theory]
    [InlineData("+5541999991234")]
    [InlineData("+5511988884321")]
    public async void Dado_Um_PhoneNumber_Existente_Trás_Do_Banco(string phone)
    {
        var result = await _userQuery.GetByPhoneNumberAsync(phone);

        Assert.NotNull(result);
        Assert.True(_users.Any(x => x.PhoneNumber.Value == phone));
    }

    [Theory]
    [InlineData("+5562969445563")]
    [InlineData("+5593981777179")]
    [InlineData("+5535987832132")]
    public async void Dado_Um_PhoneNumber_Inexistente_Não_Trás_Do_Banco(string phone)
    {
        var result = await _userQuery.GetByPhoneNumberAsync(phone);

        Assert.Null(result);
        Assert.True(!_users.Any(x => x.PhoneNumber.Value == phone));
    }
}