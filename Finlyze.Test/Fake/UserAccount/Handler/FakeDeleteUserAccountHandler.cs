using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFakes;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFakes;

namespace Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFakes;

public class FakeDeleteUserAccountHandler : IDeleteAccountHandler
{
    private readonly List<UserAccount> _users = new();
    private readonly FakeUserAccountQuery _userQuery;
    private readonly FakeUserAccountRepository _userRepository;

    public FakeDeleteUserAccountHandler(List<UserAccount> users)
    {
        _users = users;
        _userQuery = new FakeUserAccountQuery(_users);
        _userRepository = new FakeUserAccountRepository(_users);
    }

    public async Task<ResultHandler<UserAccount>> Handle(DeleteUserAccountCommand command)
    {
        var existingUser = await _userQuery.GetByIdAsync(command.Id);

        if (existingUser is null)
            return ResultHandler<UserAccount>.Fail("Conta do usuário com esse Id não encontrado.");

        var rows = await _userRepository.DeleteAsync(existingUser);

        if (rows == 0)
            return ResultHandler<UserAccount>.Fail("Erro ao deletar conta do usuário.");

        return ResultHandler<UserAccount>.Ok("Conta do usuário deletado com sucesso.", existingUser);
    }
}
