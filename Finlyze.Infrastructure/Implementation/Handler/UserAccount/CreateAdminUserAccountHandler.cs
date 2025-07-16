using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Application.Abstract.Interface.Handler;

public class CreateAdminUserAccountHandler : ICreateAdminUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly IAppLogRepository _appRepository;

    public CreateAdminUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(CreateUserAccountCommand command)
    {
        try
        {
            var existingEmail = await _userQuery.GetByEmailAsync(command.Email);

            if (existingEmail is not null)
                return ResultHandler<UserAccount>.Fail("Email já está em uso.");

            var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingPhone is not null)
                return ResultHandler<UserAccount>.Fail("PhoneNumber já está em uso.");

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate, true, (int)(ERole.User | ERole.Admin));

            var row = await _userRepository.CreateAsync(userAccount);

            if (row == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "UserAccount", $"Erro ao criar essa conta de administrador com email '{command.Email}'"));
                return ResultHandler<UserAccount>.Fail("Falha ao criar conta do usuário administrador.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "UserAccount", $"Conta de administrador '{userAccount.Email.Value}' criado com sucesso"));

            return ResultHandler<UserAccount>.Ok("Conta criada com sucesso.", null);
        }

        catch (Exception ex) when (ex is DomainException or EmailRegexException or PhoneNumberRegexException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Validation, "UserAccount", $"Erro na validação ao criar a conta de administrador com email '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "UserAccount", $"Erro ao criar a conta de administrador com email '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail($"Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}
