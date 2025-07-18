using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Application.Authentication.Hasher;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class UpdateUserAccountHandler : IUpdateUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly ISystemLogRepository _systemRepository;

    public UpdateUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, ISystemLogRepository systemRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(UpdateUserAccountCommand command)
    {
        try
        {
            var userAccount = await _userQuery.GetByIdAsync(command.Id);

            if (userAccount is null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "UserAccount", $"Não foi possível atualizar a conta: ID '{command.Id}' não encontrado."));
                return ResultHandler<UserAccount>.Fail("Conta de usuário com esse ID não foi encontrada.");
            }

            if (!userAccount.Password.Value.VerifyHash(command.ConfirmPassword))
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Falha ao atualizar conta do usuário ID '{command.Id}': senha incorreta na confirmação."));
                return ResultHandler<UserAccount>.Fail("Senha incorreta.");
            }

            userAccount.ChangeName(command.Name);
            userAccount.ChangeEmail(command.Email);
            userAccount.ChangePhoneNumber(command.PhoneNumber);
            userAccount.ChangeBirthDate(command.BirthDate);

            if (!userAccount.Password.Value.VerifyHash(command.Password) && !string.IsNullOrWhiteSpace(command.Password))
                userAccount.ChangePassword(command.Password);

            var rows = await _userRepository.UpdateAsync(userAccount);

            if (rows == 0)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Falha ao atualizar conta do usuário ID '{command.Id}'."));
                return ResultHandler<UserAccount>.Fail("Falha ao atualizar a conta.");
            }

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "UserAccount", $"Conta de usuário ID '{userAccount.Id}' atualizada com sucesso."));

            return ResultHandler<UserAccount>.Ok("Conta atualizada com sucesso.", userAccount);
        }

        catch (Exception ex) when (ex is DomainException or EmailRegexException or PhoneNumberRegexException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Validation, "UserAccount", $"Erro de validação ao atualizar conta do usuário ID '{command.Id}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "UserAccount", $"Erro inesperado ao atualizar conta do usuário ID '{command.Id}': {errorMsg}"));
            return ResultHandler<UserAccount>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}