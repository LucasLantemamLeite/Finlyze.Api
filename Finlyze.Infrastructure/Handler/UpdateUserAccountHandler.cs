using System.ComponentModel;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.UserAccountObject;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class UpdateUserAccountHandler : IUpdateUserAcconutHandler
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

    public async Task<ResultPattern<UserAccount>> Handle(UpdateUserAccountCommand command)
    {
        try
        {
            var user = await _userQuery.GetByIdAsync(command.Id);

            if (user.Data == null)
                return ResultPattern<UserAccount>.Fail("Nenhuma conta encontrada.");

            var userData = user.Data;

            userData.ChangeName(command.Name);
            userData.ChangeEmail(command.Email);
            userData.ChangePassword(command.Password);
            userData.ChangePhoneNumber(command.PhoneNumber);

            var result = await _userRepository.UpdateAsync(userData);

            if (!result.Success)
            {
                var appLogError = new AppLog(3, "UpdateUserAccount", $"Falha ao atualizar conta do usuário, erro recebido: {result.Message}");
                await _appRepository.CreateAsync(appLogError);
                return ResultPattern<UserAccount>.Fail($"DeleteUserAccountHandler -> Handle {result.Message}");
            }

            var appLog = new AppLog(1, "UpdateUserAccount", "Conta atualizada com sucesso.");
            await _appRepository.CreateAsync(appLog);
            return result;
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada do Handler.";
            var appLogException = new AppLog(3, $"Exeção do Handle de UpdateUserAccountHandler", errorMsg);
            await _appRepository.CreateAsync(appLogException);
            return ResultPattern<UserAccount>.Fail($"Handler -> DeleteUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}