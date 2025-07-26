using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFake;

public class FakeUserAccountQuery : IUserAccountQuery
{
    private readonly List<UserAccount> _users = new();

    public FakeUserAccountQuery(List<UserAccount> users) => _users = users;

    public Task<UserAccount?> GetByEmailAsync(string email)
    {
        var userAccount = _users.FirstOrDefault(x => x.Email.Value == email);

        return Task.FromResult(userAccount);
    }

    public Task<UserAccount?> GetByIdAsync(Guid id)
    {
        var userAccount = _users.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(userAccount);
    }

    public Task<UserAccount?> GetByPhoneNumberAsync(string phone)
    {
        var userAccount = _users.FirstOrDefault(x => x.PhoneNumber.Value == phone);

        return Task.FromResult(userAccount);
    }
}