using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Application.Authentication.Hasher;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class LoginUserAccountHandler : ILoginUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly IAppLogRepository _appRepository;

    public LoginUserAccountHandler(
        IUserAccountQuery userQuery,
        IUserAccountRepository userRepository,
        IAppLogRepository appRepository)
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

            if (userAccount is null || !userAccount.Password.Value.VerifyHash(command.Password))
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Login", $"Falha no login para o e-mail: {command.Email}"));
                return ResultHandler<UserAccount>.Fail("Credenciais incorretas.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Login", $"Login realizado com sucesso para o e-mail: {command.Email}"));

            return ResultHandler<UserAccount>.Ok("Login realizado com sucesso.", userAccount);
        }
        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Login", $"Exceção no login para o e-mail: {command.Email} -> {errorMsg}"));
            return ResultHandler<UserAccount>.Fail($"LoginUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}
