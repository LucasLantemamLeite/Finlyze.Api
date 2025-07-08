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
    private readonly IAppLogRepository _appRepository;

    public UpdateUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<UserAccount>> Handle(UpdateUserAccountCommand command)
    {
        try
        {
            var userAccount = await _userQuery.GetByIdAsync(command.Id);

            if (userAccount is null)
                return ResultHandler<UserAccount>.Fail("Conta não encontrada.");

            if (!userAccount.Password.Value.VerifyHash(command.ConfirmPassword))
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Update", $"Senha incorreta na tentativa de atualização da conta ID: {command.Id}"));
                return ResultHandler<UserAccount>.Fail("Senha incorreta.");
            }

            userAccount.ChangeName(command.Name);
            userAccount.ChangeEmail(command.Email);
            userAccount.ChangePhoneNumber(command.PhoneNumber);
            userAccount.ChangeBirthDate(command.BirthDate);

            if (!userAccount.Password.Value.VerifyHash(command.Password) && !string.IsNullOrWhiteSpace(command.Password))
                userAccount.ChangePassword(command.Password);

            var row = await _userRepository.UpdateAsync(userAccount);

            if (row == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Update", $"Falha ao atualizar conta ID: {command.Id}"));
                return ResultHandler<UserAccount>.Fail("Falha ao atualizar a conta.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Update", $"Conta '{userAccount.Id}' atualizada com sucesso."));

            return ResultHandler<UserAccount>.Ok("Conta atualizada com sucesso.", userAccount);
        }

        catch (Exception ex) when (ex is DomainException or EmailRegexException or PhoneNumberRegexException or EnumException)
        {
            var msg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Validation, "Update", $"Erro de validação ao atualizar conta ID: {command.Id} -> {msg}"));
            return ResultHandler<UserAccount>.Fail(msg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Update", $"Erro inesperado ao atualizar conta ID: {command.Id} -> {errorMsg}"));
            return ResultHandler<UserAccount>.Fail($"Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}