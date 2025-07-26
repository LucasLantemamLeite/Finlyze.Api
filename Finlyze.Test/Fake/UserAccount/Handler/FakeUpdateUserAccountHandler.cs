using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Test.Abstract.Interfaces.Queries.Fakes.UserAccountFakes;
using Finlyze.Test.Abstract.Interfaces.Repositories.Fakes.UserAccountFakes;

namespace Finlyze.Test.Abstract.Interfaces.Handlers.Fakes.UserAccountFakes;

public class FakeUpdateUserAccountHandler : IUpdateUserAccountHandler
{
    private readonly List<UserAccount> _users = new();
    private readonly FakeUserAccountQuery _userQuery;
    private readonly FakeUserAccountRepository _userRepository;

    public FakeUpdateUserAccountHandler(List<UserAccount> users)
    {
        _users = users;
        _userQuery = new FakeUserAccountQuery(_users);
        _userRepository = new FakeUserAccountRepository(_users);
    }

    public async Task<ResultHandler<UserAccount>> Handle(UpdateUserAccountCommand command)
    {
        var existingUser = await _userQuery.GetByIdAsync(command.Id);

        if (existingUser is null)
            return ResultHandler<UserAccount>.Fail("Conta do usuário com esse Id não encontrado.");

        if (existingUser.Password.Value != command.ConfirmPassword)
            return ResultHandler<UserAccount>.Fail("Senha incorreta.");

        existingUser.ChangeName(command.Name);
        existingUser.ChangeEmail(command.Email);
        existingUser.ChangePassword(command.Password);
        existingUser.ChangePhoneNumber(command.PhoneNumber);
        existingUser.ChangeBirthDate(command.BirthDate);

        var rows = await _userRepository.UpdateAsync(existingUser);

        if (rows == 0)
            return ResultHandler<UserAccount>.Fail("Erro ao atualizar a conta do usuário.");

        return ResultHandler<UserAccount>.Ok("Conta do usuário atualizado com sucesso.", existingUser);
    }
}