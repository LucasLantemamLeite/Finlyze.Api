using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class CreateAdmimUserAccountHandler : ICreateAdminUserAccountHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IUserAccountRepository _userRepository;
    private readonly IAppLogRepository _appRepository;

    public CreateAdmimUserAccountHandler(IUserAccountQuery userQuery, IUserAccountRepository userRepository, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _userRepository = userRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultPattern<UserAccount>> Handle(CreateUserAccountCommand command)
    {
        try
        {
            var existingEmail = await _userQuery.GetByEmailAsync(command.Email);

            if (existingEmail.Data != null)
                return ResultPattern<UserAccount>.Fail("Email já está em uso.");

            var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingPhone.Data != null)
                return ResultPattern<UserAccount>.Fail("PhoneNumber já está em uso.");

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate, true, 2);

            var result = await _userRepository.CreateAsync(userAccount);

            if (!result.Success)
            {
                var appLogError = new AppLog(3, "CreateAdminUserAccount", $"Falha ao criar conta do usuário Admin, erro recebido: {result.Message}.");
                await _appRepository.CreateAsync(appLogError);
                return ResultPattern<UserAccount>.Fail(result.Message);
            }

            return result;
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada do Handler.";
            var appLogException = new AppLog(3, "Exceção no CreateUserAccountHandler", errorMsg);
            await _appRepository.CreateAsync(appLogException);
            return ResultPattern<UserAccount>.Fail($"Handler -> CreateUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}