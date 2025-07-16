using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class DeleteUserAccountHandler : IDeleteAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly IAppLogRepository _appRepository;
    public DeleteUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(DeleteUserAccountCommand command)
    {
        try
        {
            var existingUser = await _userQuery.GetByIdAsync(command.Id);

            if (existingUser is null)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Warning, "UserAccount", $"Erro ao deletar conta do usuário do Id '{command.Id}': Conta de usuário com esse Id não encontrada"));
                return ResultHandler<UserAccount>.Fail("Conta de usuário com esse Id não encontrada.");
            }

            var row = await _userRepository.DeleteAsync(existingUser);

            if (row == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "UserAccount", $"Erro ao deletar conta do usuário do Id '{existingUser.Id}'"));
                return ResultHandler<UserAccount>.Fail("Falha ao deletar conta do usuário.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "UserAccount", $"Conta do usuário do Id '{existingUser.Id}' deletado com sucesso"));

            return ResultHandler<UserAccount>.Ok("Conta deletada com sucesso.", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "UserAccount", $"Erro ao deletar a conta de usuário do Id '{command.Id}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail($"Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}