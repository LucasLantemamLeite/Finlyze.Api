using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;

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

            if (existingEmail != null)
                return ResultHandler<UserAccount>.Fail("Email já está em uso.");

            var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingPhone != null)
                return ResultHandler<UserAccount>.Fail("PhoneNumber já está em uso.");

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate);

            var rows = await _userRepository.CreateAsync(userAccount);

            if (rows == 0)
            {
                var appLogError = new AppLog(3, "Create", "Não foi possível criar essa conta do usuário");
                await _appRepository.CreateAsync(appLogError);
                return ResultHandler<UserAccount>.Fail("Falha ao criar conta do usuário");
            }

            var appLogSucess = new AppLog(1, "Create", $"Usuario: {userAccount.Email.Value} criado com sucesso");
            await _appRepository.CreateAsync(appLogSucess);

            return ResultHandler<UserAccount>.Ok("Conta criada com sucesso.", userAccount);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            var appLogException = new AppLog(3, "Create", $"{errorMsg}");
            await _appRepository.CreateAsync(appLogException);
            return ResultHandler<UserAccount>.Fail($"CreateUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}