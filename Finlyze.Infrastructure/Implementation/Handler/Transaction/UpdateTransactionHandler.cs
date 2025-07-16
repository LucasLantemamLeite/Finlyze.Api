using System.ComponentModel;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class UpdateTransactionHandler : IUpdateTransactionHandler
{
    private readonly ITransactionQuery _tranQuery;
    private readonly ITransactionRepository _tranRepository;
    private readonly IAppLogRepository _appRepository;

    public UpdateTransactionHandler(ITransactionQuery tranQuery, ITransactionRepository tranRepository, IAppLogRepository appRepository)
    {
        _tranQuery = tranQuery;
        _tranRepository = tranRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<Transaction>> Handle(UpdateTransactionCommand command)
    {
        try
        {
            var transaction = await _tranQuery.GetByIdAsync(command.Id);

            if (transaction is null)
                return ResultHandler<Transaction>.Fail("Transaction com esse Id não encontrado.");

            transaction.ChangeTitle(command.TransactionTitle);
            transaction.ChangeDescription(command.TransactionDescription);
            transaction.ChangeAmount(command.Amount);
            transaction.ChangeType(command.TypeTransaction);
            transaction.ChangeCreate(command.TransactionCreateAt);

            var rows = await _tranRepository.UpdateAsync(transaction);

            if (rows == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Transaction", $"Erro ao atualizar Transaction da conta de usuário com Id '{transaction.UserAccountId}'"));
                return ResultHandler<Transaction>.Fail("Falha ao atualizar a Transaction");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Transaction", $"Transaction do Id '{transaction.Id}' atualizado com sucesso"));

            return ResultHandler<Transaction>.Ok("Transaction Atualizada com sucesso.", null);
        }

        catch (Exception ex) when (ex is DomainException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Validation, "Transaction", $"Erro de validação ao atualizar Transaction do usuário do Id '{command.Id}': {errorMsg}"));
            return ResultHandler<Transaction>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Transaction", $"Erro ao atualizar a Transaction do usuário do Id '{command.Id}': {errorMsg}"));
            return ResultHandler<Transaction>.Fail($"Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}