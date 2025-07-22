using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFake;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFake;

namespace Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFake;

public class FakeCreateUserAccountHandler : ICreateUserAccountHandler
{
    private readonly List<UserAccount> _users = new();
    private readonly FakeUserAccountQuery _userQuery;
    private readonly FakeUserAccountRepository _userRepository;

    public FakeCreateUserAccountHandler(List<UserAccount> users)
    {
        _users = users;
        _userQuery = new FakeUserAccountQuery(users);
        _userRepository = new FakeUserAccountRepository(users);
    }

    public async Task<ResultHandler<UserAccount>> Handle(CreateUserAccountCommand command)
    {
        var existingEmail = await _userQuery.GetByEmailAsync(command.Email);

        if (existingEmail is not null)
            return ResultHandler<UserAccount>.Fail("Email já está sendo usado.");

        var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

        if (existingPhone is not null)
            return ResultHandler<UserAccount>.Fail("Número de telefone já está sendo usado.");

        var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate);
        var rows = await _userRepository.CreateAsync(userAccount);

        if (rows == 0)
            return ResultHandler<UserAccount>.Fail("Erro ao criar conta do usuário.");

        return ResultHandler<UserAccount>.Ok("Conta do usuário criado com sucesso.", userAccount);
    }
}