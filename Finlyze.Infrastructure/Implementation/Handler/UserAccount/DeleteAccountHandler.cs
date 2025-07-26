using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Application.Abstracts.Interfaces.Repositories;
using Finlyze.Domain.Entities.SystemLogEntity;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Domain.ValueObjects.Enums;

namespace Finlyze.Infrastructure.Implementations.Interfaces.Handlers;

public class DeleteUserAccountHandler : IDeleteAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly ISystemLogRepository _systemRepository;

    public DeleteUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, ISystemLogRepository systemRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(DeleteUserAccountCommand command)
    {
        try
        {
            var existingUser = await _userQuery.GetByIdAsync(command.Id);

            if (existingUser is null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "UserAccount", $"Não foi possível deletar a conta: ID '{command.Id}' não encontrado."));
                return ResultHandler<UserAccount>.Fail("Conta de usuário com esse ID não foi encontrada.");
            }

            var rows = await _userRepository.DeleteAsync(existingUser);

            if (rows == 0)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Falha ao deletar conta de usuário: ID '{existingUser.Id}'."));
                return ResultHandler<UserAccount>.Fail("Falha ao deletar conta do usuário.");
            }

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "UserAccount", $"Conta de usuário deletada com sucesso: ID '{existingUser.Id}'."));

            return ResultHandler<UserAccount>.Ok("Conta deletada com sucesso.", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Erro inesperado ao deletar conta de usuário com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}