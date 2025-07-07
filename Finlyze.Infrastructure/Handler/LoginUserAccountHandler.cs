using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Authentication.Hasher;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class LoginUserAccountHandler : ILoginUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly IAppLogRepository _appRepository;
    public LoginUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultPattern<UserAccount>> Handle(LoginUserAccountCommand command)
    {
        try
        {
            var result = await _userQuery.GetByEmailAsync(command.Email);

            if (result.Data == null || !result.Data.Password.Value.VerifyHash(command.Password))
                return ResultPattern<UserAccount>.Fail("Credenciais incorretas.");

            var appLog = new AppLog(1, "Login do Usuário", "Realizado com sucesso.");
            await _appRepository.CreateAsync(appLog);
            return ResultPattern<UserAccount>.Ok(null, result.Data);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada do Handler.";
            var appLogException = new AppLog(3, $"Exeção do Handle de DeleteUserAccountHandler", errorMsg);
            await _appRepository.CreateAsync(appLogException);
            return ResultPattern<UserAccount>.Fail($"Handler -> LoginUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}