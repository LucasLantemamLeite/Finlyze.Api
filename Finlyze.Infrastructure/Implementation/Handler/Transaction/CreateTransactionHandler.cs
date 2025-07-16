using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class CreateTransactionHandler : ICreateTransactionHandler
{
    private readonly IUserAccountQuery _userQuery;
    private readonly ITransactionRepository _transRepository;
    private readonly IAppLogRepository _appRepository;

    public CreateTransactionHandler(IUserAccountQuery userQuery, ITransactionRepository transReposotory, IAppLogRepository appRepository)
    {
        _userQuery = userQuery;
        _transRepository = transReposotory;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<Transaction>> Handle(CreateTransactionCommand command)
    {
        try
        {
            var existingUser = await _userQuery.GetByIdAsync(command.UserAccountId);

            if (existingUser is null)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Warning, "Transaction", $"Falha ao criar transação: usuário com ID '{command.UserAccountId}' não encontrado."));
                return ResultHandler<Transaction>.Fail("Usuário com esse ID não foi encontrado.");
            }

            var transaction = new Transaction(command.TransactionTitle, command.TransactionDescription, command.Amount, command.TypeTransaction, command.TransactionCreateAt, command.UserAccountId);

            var id = await _transRepository.UpdateAsync(transaction);

            transaction.ChangeId(id);

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Transaction", $"Transação criada com sucesso: ID '{transaction.Id}' vinculada ao usuário '{transaction.UserAccountId}'."));

            return ResultHandler<Transaction>.Ok("Transação criada com sucesso.", transaction);
        }

        catch (Exception ex) when (ex is DomainException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Validation, "Transaction", $"Falha de validação ao criar transação para o usuário '{command.UserAccountId}': {errorMsg}"));
            return ResultHandler<Transaction>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Transaction", $"Erro inesperado ao criar transação para o usuário '{command.UserAccountId}': {errorMsg}"));
            return ResultHandler<Transaction>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}