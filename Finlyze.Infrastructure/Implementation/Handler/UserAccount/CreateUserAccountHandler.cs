using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class CreateUserAccountHandler : ICreateUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly IAppLogRepository _appRepository;

    public CreateUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, IAppLogRepository appRepository)
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
            {
                await _appRepository.CreateAsync(new SystemLog((int)ELog.Warning, "UserAccount", $"Não foi possível criar conta de usuário: e-mail '{command.Email}' já está em uso."));
                return ResultHandler<UserAccount>.Fail("Email já está em uso.");
            }

            var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingPhone is not null)
            {
                await _appRepository.CreateAsync(new SystemLog((int)ELog.Warning, "UserAccount", $"Não foi possível criar conta de usuário: telefone '{command.PhoneNumber}' já está em uso."));
                return ResultHandler<UserAccount>.Fail("Número de telefone já está em uso.");
            }

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate);

            var rows = await _userRepository.CreateAsync(userAccount);

            if (rows == 0)
            {
                await _appRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Falha ao criar conta de usuário com e-mail '{command.Email}'."));
                return ResultHandler<UserAccount>.Fail("Falha ao criar conta do usuário.");
            }

            await _appRepository.CreateAsync(new SystemLog((int)ELog.Info, "UserAccount", $"Conta de usuário '{userAccount.Email.Value}' criada com sucesso."));

            return ResultHandler<UserAccount>.Ok("Conta criada com sucesso.", userAccount);
        }

        catch (Exception ex) when (ex is DomainException or EmailRegexException or PhoneNumberRegexException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new SystemLog((int)ELog.Validation, "UserAccount", $"Erro de validação ao criar conta de usuário com e-mail '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _appRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Erro inesperado ao criar conta de usuário com e-mail '{command.Email}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}