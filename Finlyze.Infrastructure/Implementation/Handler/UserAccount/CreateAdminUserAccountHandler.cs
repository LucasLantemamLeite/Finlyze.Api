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
    private readonly ISystemLogRepository _systemRepository;

    public CreateAdminUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, ISystemLogRepository systemRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(CreateUserAccountCommand command)
    {
        try
        {
            var existingEmail = await _userQuery.GetByEmailAsync(command.Email);

            if (existingEmail is not null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "UserAccount", $"Não foi possível criar conta de administrador: e-mail '{command.Email}' já está em uso."));
                return ResultHandler<UserAccount>.Fail("Email já está em uso.");
            }

            var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingPhone is not null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "UserAccount", $"Não foi possível criar conta de administrador: telefone '{command.PhoneNumber}' já está em uso."));
                return ResultHandler<UserAccount>.Fail("PhoneNumber já está em uso.");
            }

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate, true, (int)(ERole.User | ERole.Admin));

            var rows = await _userRepository.CreateAsync(userAccount);

            if (rows == 0)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Falha ao criar conta de administrador com e-mail '{command.Email}'."));
                return ResultHandler<UserAccount>.Fail("Falha ao criar conta do usuário administrador.");
            }

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "UserAccount", $"Conta de administrador '{userAccount.Email.Value}' criada com sucesso."));

            return ResultHandler<UserAccount>.Ok("Conta criada com sucesso.", null);
        }

        catch (Exception ex) when (ex is DomainException or EmailRegexException or PhoneNumberRegexException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Validation, "UserAccount", $"Erro de validação ao criar conta de administrador com e-mail '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Erro inesperado ao criar conta de administrador com e-mail '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}