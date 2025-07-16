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

    public async Task<ResultHandler<FinancialTransaction>> Handle(UpdateTransactionCommand command)
    {
        try
        {
            var transaction = await _tranQuery.GetByIdAsync(command.Id);

            if (transaction is null)
            {
                await _appRepository.CreateAsync(new SystemLog((int)ELog.Warning, "FinancialTransaction", $"Não foi possível atualizar a transação: ID '{command.Id}' não encontrado."));
                return ResultHandler<FinancialTransaction>.Fail("Transação com esse ID não foi encontrada.");
            }

            transaction.ChangeTitle(command.Title);
            transaction.ChangeDescription(command.Description);
            transaction.ChangeAmount(command.Amount);
            transaction.ChangeType(command.TranType);
            transaction.ChangeCreate(command.CreateAt);

            var rows = await _tranRepository.UpdateAsync(transaction);

            if (rows == 0)
            {
                await _appRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Falha ao atualizar transação: ID '{transaction.Id}' do usuário '{transaction.UserAccountId}'."));
                return ResultHandler<FinancialTransaction>.Fail("Falha ao atualizar a transação.");
            }

            await _appRepository.CreateAsync(new SystemLog((int)ELog.Info, "FinancialTransaction", $"Transação atualizada com sucesso: ID '{transaction.Id}' vinculada ao usuário '{transaction.UserAccountId}'."));

            return ResultHandler<FinancialTransaction>.Ok("Transação atualizada com sucesso.", null);
        }

        catch (Exception ex) when (ex is DomainException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _appRepository.CreateAsync(new SystemLog((int)ELog.Validation, "FinancialTransaction", $"Erro de validação ao atualizar transação com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _appRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Erro inesperado ao atualizar transação com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}