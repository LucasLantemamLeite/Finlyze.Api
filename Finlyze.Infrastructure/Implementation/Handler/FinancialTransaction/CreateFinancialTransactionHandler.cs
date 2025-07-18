using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class CreateFinancialTransactionHandler : ICreateFinancialTransactionHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly IFinancialTransactionRepository _tranRepository;
    private readonly ISystemLogRepository _systemRepository;

    public CreateFinancialTransactionHandler(IUserAccountQuery userQuery, IFinancialTransactionRepository tranReposotory, ISystemLogRepository systemRepository)
    {
        _userQuery = userQuery;
        _tranRepository = tranReposotory;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<FinancialTransaction>> Handle(CreateTransactionCommand command)
    {
        try
        {
            var existingUser = await _userQuery.GetByIdAsync(command.UserAccountId);

            if (existingUser is null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "FinancialTransaction", $"Falha ao criar transação: usuário com ID '{command.UserAccountId}' não encontrado."));
                return ResultHandler<FinancialTransaction>.Fail("Usuário com esse ID não foi encontrado.");
            }

            var transaction = new FinancialTransaction(command.Title, command.Description, command.Amount, command.TranType, command.CreateAt, command.UserAccountId);

            var id = await _tranRepository.UpdateAsync(transaction);

            transaction.ChangeId(id);

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "FinancialTransaction", $"Transação criada com sucesso: ID '{transaction.Id}' vinculada ao usuário '{transaction.UserAccountId}'."));

            return ResultHandler<FinancialTransaction>.Ok("Transação criada com sucesso.", transaction);
        }

        catch (Exception ex) when (ex is DomainException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Validation, "FinancialTransaction", $"Falha de validação ao criar transação para o usuário '{command.UserAccountId}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Erro inesperado ao criar transação para o usuário '{command.UserAccountId}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}