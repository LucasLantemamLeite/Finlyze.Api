using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
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

    public async Task<ResultHandler<UserAccount>> Handle(LoginUserAccountCommand command)
    {
        try
        {
            var userAccount = await _userQuery.GetByEmailAsync(command.Email);

            if (userAccount == null || !userAccount.Password.Value.VerifyHash(command.Password))
            {
                var appLogError = new AppLog(3, "Login", "Falha ao realizar login");
                await _appRepository.CreateAsync(appLogError);
                return ResultHandler<UserAccount>.Fail("Credenciais incorretas.");
            }

            var appLogSucess = new AppLog(1, "Login", $"Login realizado com sucesso");
            await _appRepository.CreateAsync(appLogSucess);

            return ResultHandler<UserAccount>.Ok("Login realizado com sucesso.", userAccount);
        }
        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            var appLogException = new AppLog(3, "Login", $"{errorMsg}");
            await _appRepository.CreateAsync(appLogException);
            return ResultHandler<UserAccount>.Fail($"LoginUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}