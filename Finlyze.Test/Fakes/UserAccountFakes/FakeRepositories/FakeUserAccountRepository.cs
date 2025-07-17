using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;

namespace Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFake;

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

        user.ChangeName(user.Name.Value);
        user.ChangeEmail(user.Email.Value);
        user.ChangePassword(user.Password.Value);
        user.ChangePhoneNumber(user.PhoneNumber.Value);
        user.ChangeBirthDate(user.BirthDate.Value);

        _users[index] = user;

        return Task.FromResult(1);
    }
}