using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
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

    public async Task<ResultPattern<UserAccount>> Handle(DeleteUserAccountCommand command)
    {
        try
        {
            var userAccount = await _userQuery.GetByIdAsync(command.Id);

            if (userAccount == null)
                return ResultPattern<UserAccount>.Fail("Conta não encontrada");

            var result = await _userRepository.DeleteAsync(userAccount.Data);

            if (!result.Success)
            {
                var appLogError = new AppLog(3, "DeleteUserAccount", $"Falha ao deletar conta de usuário, erro recebido: {result.Message}");
                await _appRepository.CreateAsync(appLogError);
                return ResultPattern<UserAccount>.Fail($"DeleteUserAccountHandler -> Handle {result.Message}");
            }

            var appLog = new AppLog(1, "DeleteUserAccount", "Conta deletada com sucesso.");
            await _appRepository.CreateAsync(appLog);

            return result;
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada do Handler.";
            var appLogException = new AppLog(3, $"Exeção do Handle de DeleteUserAccountHandler", errorMsg);
            await _appRepository.CreateAsync(appLogException);
            return ResultPattern<UserAccount>.Fail($"Handler -> DeleteUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}