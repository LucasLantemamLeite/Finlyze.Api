using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFake;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFake;

namespace Finlyze.Test.Handlers.Fakes.UserAccountFake;

[Trait("Category", "UserAccountHandler")]
public class UserAccountHandlerTest
{
    private List<UserAccount> _users = new();
    private FakeUserAccountQuery _userQuery;
    private FakeUserAccountRepository _userRepository;

    public UserAccountHandlerTest()
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
        _userRepository = new FakeUserAccountRepository(_users);
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Válido_Adiciona_Na_Entidade()
    {
        var command = new CreateUserAccountCommand("Lucas", "lucas.leite2@finlyze.com.br", "senha1234", "+5591998916388", DateOnly.FromDateTime(new DateTime(2005, 04, 08)));

        var existingEmail = await _userQuery.GetByEmailAsync(command.Email);
        var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

        Assert.False(_users.Any(x => x.Email.Value == command.Email || x.PhoneNumber.Value == command.PhoneNumber));

        var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate);
        var result = await _userRepository.CreateAsync(userAccount);

        Assert.Equal(1, result);
        Assert.True(_users.Any(x => x.Email.Value == command.Email));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_Email_Já_Em_Uso_Não_Adiciona_Na_Entidade()
    {
        var command = new CreateUserAccountCommand("Lucas", "maria.silva@finlyze.com", "senha1234", "+5591998916388", DateOnly.FromDateTime(new DateTime(2005, 04, 08)));

        var existingEmail = await _userQuery.GetByEmailAsync(command.Email);
        var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

        Assert.True(_users.Any(x => x.Email.Value == command.Email || x.PhoneNumber.Value == command.PhoneNumber));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_PhoneNumber_Já_Em_Uso_Não_Adiciona_Na_Entidade()
    {
        var command = new CreateUserAccountCommand("Lucas", "lucas.leite2@finlyze.com.br", "senha1234", "+5511988884321", DateOnly.FromDateTime(new DateTime(2005, 04, 08)));

        var existingEmail = await _userQuery.GetByEmailAsync(command.Email);
        var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

        Assert.True(_users.Any(x => x.Email.Value == command.Email || x.PhoneNumber.Value == command.PhoneNumber));
    }
}