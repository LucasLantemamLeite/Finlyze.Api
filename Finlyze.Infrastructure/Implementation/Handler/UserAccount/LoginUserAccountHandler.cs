using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Application.Authentication.Hasher;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class LoginUserAccountHandler : ILoginUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly ISystemLogRepository _systemRepository;

    public LoginUserAccountHandler(IUserAccountQuery userQuery, ISystemLogRepository systemRepository)
    {
        _userQuery = userQuery;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(LoginUserAccountCommand command)
    {
        try
        {
            var userAccount = await _userQuery.GetByEmailAsync(command.Email);

            if (userAccount is null || !userAccount.Password.Value.VerifyHash(command.Password))
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Falha no login: credenciais inválidas para o e-mail '{command.Email}'."));
                return ResultHandler<UserAccount>.Fail("Credenciais incorretas.");
            }

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "UserAccount", $"Login bem-sucedido: usuário com e-mail '{command.Email}'."));

            return ResultHandler<UserAccount>.Ok("Login realizado com sucesso.", userAccount);
        }

        catch (Exception ex) when (ex is DomainException or EmailRegexException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Validation, "UserAccount", $"Erro de validação no login com e-mail '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Erro inesperado ao fazer login com e-mail '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}