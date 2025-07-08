using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;

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
                return ResultHandler<UserAccount>.Fail("Email já está em uso.");

            var existingPhone = await _userQuery.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingPhone is not null)
                return ResultHandler<UserAccount>.Fail("Número de telefone já está em uso.");

            var userAccount = new UserAccount(command.Name, command.Email, command.Password, command.PhoneNumber, command.BirthDate);

            var rows = await _userRepository.CreateAsync(userAccount);

            if (rows == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "CreateUserAccount", $"Falha ao criar conta para o email: {command.Email}"));
                return ResultHandler<UserAccount>.Fail("Falha ao criar conta do usuário.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "CreateUserAccount", $"Usuário com email {userAccount.Email.Value} criado com sucesso."));

            return ResultHandler<UserAccount>.Ok("Conta criada com sucesso.", userAccount);
        }
        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "CreateUserAccount", $"CreateUserAccountHandler -> Handle: {errorMsg}"));
            return ResultHandler<UserAccount>.Fail($"Erro interno: {errorMsg}");
        }
    }
}