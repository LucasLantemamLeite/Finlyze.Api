using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFake;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFake;

namespace Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFake;

public class FakeUserAccountLoginHandler : ILoginUserAccountHandler
{
    private readonly List<UserAccount> _users = new();
    private readonly FakeUserAccountQuery _userQuery;
    private readonly FakeUserAccountRepository _userRepository;

    public FakeUserAccountLoginHandler(List<UserAccount> users)
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