using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFakes;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFakes;

namespace Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFakes;

public class FakeLoginUserAccountHandler : ILoginUserAccountHandler
{
    private readonly List<UserAccount> _users = new();
    private readonly FakeUserAccountQuery _userQuery;
    private readonly FakeUserAccountRepository _userRepository;

    public FakeLoginUserAccountHandler(List<UserAccount> users)
    {
        _users = users;
        _userQuery = new FakeUserAccountQuery(_users);
        _userRepository = new FakeUserAccountRepository(_users);
    }

    public async Task<ResultHandler<UserAccount>> Handle(LoginUserAccountCommand command)
    {
        var existingUser = await _userQuery.GetByEmailAsync(command.Email);

        if (existingUser is null || existingUser.Password.Value != command.Password)
            return ResultHandler<UserAccount>.Fail("Credenciais Incorretas");

        return ResultHandler<UserAccount>.Ok("Login realizado com sucesso.", existingUser);

    }
}