using Finlyze.Application.Abstracts.Interfaces.Repositories;
using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFakes;

public class FakeUserAccountRepository : IUserAccountRepository
{
    private readonly List<UserAccount> _users = new();

    public FakeUserAccountRepository(List<UserAccount> users) => _users = users;

    public Task<int> CreateAsync(UserAccount user)
    {
        var existingEmail = _users.FirstOrDefault(x => string.Equals(x.Email.Value.Trim(), user.Email.Value.Trim(), StringComparison.OrdinalIgnoreCase));

        if (existingEmail is not null)
            return Task.FromResult(0);

        var existingPhone = _users.FirstOrDefault(x => string.Equals(x.PhoneNumber.Value.Trim(), user.PhoneNumber.Value.Trim(), StringComparison.OrdinalIgnoreCase));

        if (existingPhone is not null)
            return Task.FromResult(0);

        _users.Add(user);

        return Task.FromResult(1);
    }

    public Task<int> DeleteAsync(UserAccount user)
    {
        var userExisting = _users.FirstOrDefault(x => x.Id == user.Id);

        if (userExisting is null)
            return Task.FromResult(0);

        _users.Remove(userExisting);

        return Task.FromResult(1);
    }

    public Task<int> UpdateAsync(UserAccount user)
    {
        var index = _users.FindIndex(x => x.Id == user.Id);
        if (index == -1)
            return Task.FromResult(0);

        var userAccount = _users[index];

        userAccount.ChangeName(user.Name.Value);
        userAccount.ChangeEmail(user.Email.Value);
        userAccount.ChangePassword(user.Password.Value);
        userAccount.ChangePhoneNumber(user.PhoneNumber.Value);
        userAccount.ChangeBirthDate(user.BirthDate.Value);

        _users[index] = userAccount;

        return Task.FromResult(1);
    }
}