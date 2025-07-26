using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFakes;

namespace Finlyze.Test.Repositories.Fakes.UserAccountFakes;

[Trait("Category", "UserAccountRepository")]
public class FakeUserAccountRepositoryTest
{
    private readonly List<UserAccount> _users;
    private readonly FakeUserAccountRepository _userRepository;

    public FakeUserAccountRepositoryTest()
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

        _userRepository = new FakeUserAccountRepository(_users);
    }


    [Fact]
    public async Task Dado_Um_UserAccount_Válido_Adiciona_No_Banco()
    {
        var userAccount = new UserAccount(
            "Ana Clara",
            "anaclara@finlyze.com.br",
            "senhaforte19871",
            "+5528975071695",
            DateOnly.FromDateTime(new DateTime(1984, 9, 13))
        );

        var result = await _userRepository.CreateAsync(userAccount);

        Assert.Equal(1, result);
        Assert.True(_users.Any(x => x.Id == userAccount.Id));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_Email_Já_Utilizado_Não_Adiciona_No_Banco()
    {
        var userAccount = new UserAccount(
            "Ana Clara",
            "maria.silva@finlyze.com",
            "senhaforte19871",
            "+5528975071695",
            DateOnly.FromDateTime(new DateTime(1984, 9, 13))
        );

        var result = await _userRepository.CreateAsync(userAccount);

        Assert.Equal(0, result);
        Assert.True(_users.Any(x => x.Email.Value == userAccount.Email.Value));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Com_PhoneNumber_Já_Utilizado_Não_Adiciona_No_Banco()
    {
        var userAccount = new UserAccount(
            "Ana Clara",
            "mariazin@gmail.com",
            "senhaforte19871",
            "+5511988884321",
            DateOnly.FromDateTime(new DateTime(1984, 9, 13))
        );

        var result = await _userRepository.CreateAsync(userAccount);

        Assert.Equal(0, result);
        Assert.True(_users.Any(x => x.PhoneNumber.Value == userAccount.PhoneNumber.Value));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Existente_Remove_Do_Banco()
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
            1
        );

        var result = await _userRepository.DeleteAsync(userAccount);

        Assert.Equal(1, result);
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
            1
        );

        var result = await _userRepository.DeleteAsync(userAccount);

        Assert.Equal(0, result);
        Assert.True(!_users.Any(x => x.Id == userAccount.Id));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Existente_Atualiza_No_Banco()
    {
        var userAccount = new UserAccount(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Mariana Atualizada",
            "mariana.atualizada@finlyze.com",
            "NovaSenha789!",
            "+5511999999999",
            DateOnly.FromDateTime(new DateTime(1999, 12, 25)),
            DateTime.UtcNow,
            true,
            1
        );

        var result = await _userRepository.UpdateAsync(userAccount);

        Assert.Equal(1, result);
        Assert.True(_users.Any(x => x.Email.Value == userAccount.Email.Value));
    }

    [Fact]
    public async Task Dado_Um_UserAccount_Inexistente_Não_Atualiza_No_Banco()
    {
        var userAccount = new UserAccount(
            Guid.Parse("22222222-2222-2222-2222-222222222223"),
            "Mariana Atualizada",
            "mariana.atualizada@finlyze.com",
            "NovaSenha789!",
            "+5511999999999",
            DateOnly.FromDateTime(new DateTime(1999, 12, 25)),
            DateTime.UtcNow,
            true,
            1
        );

        var result = await _userRepository.UpdateAsync(userAccount);

        Assert.Equal(0, result);
        Assert.True(!_users.Any(x => x.Id == userAccount.Id));
    }
}
