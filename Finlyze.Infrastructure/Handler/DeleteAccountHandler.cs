using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;

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

            if (existingUser == null)
                return ResultHandler<UserAccount>.Fail("Conta não encontrada.");

            var row = await _userRepository.DeleteAsync(existingUser);

            if (row == 0)
            {
                var appLogError = new AppLog(3, "Delete", "Não foi possível deletar essa conta do usuário");
                await _appRepository.CreateAsync(appLogError);
                return ResultHandler<UserAccount>.Fail("Falha ao deletar conta do usuário.");
            }

            var appLogSucess = new AppLog(1, "Delete", $"Usuário deletado com sucesso do banco de dados");
            await _appRepository.CreateAsync(appLogSucess);

            return ResultHandler<UserAccount>.Fail("Conta deletada com sucesso.");
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            var appLogException = new AppLog(3, "Delete", $"{errorMsg}");
            await _appRepository.CreateAsync(appLogException);
            return ResultHandler<UserAccount>.Fail($"DeleteUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}