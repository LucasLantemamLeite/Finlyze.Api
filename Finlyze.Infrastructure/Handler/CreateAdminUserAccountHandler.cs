using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;

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

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate, true, 2);

            var row = await _userRepository.CreateAsync(userAccount);

            if (row == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "CreateAdminUserAccount", "Não foi possível criar essa conta do usuário Administrador"));
                return ResultHandler<UserAccount>.Fail("Falha ao criar conta do usuário.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "CreateAdminUserAccount", $"Usuario Admin: {userAccount.Email.Value} criado com sucesso"));

            return ResultHandler<UserAccount>.Ok("Conta criada com sucesso.", userAccount);
        }
        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "CreateAdminUserAccount", errorMsg));
            return ResultHandler<UserAccount>.Fail($"CreateAdminUserAccountHandler -> Handle: {errorMsg}");
        }
    }
}
