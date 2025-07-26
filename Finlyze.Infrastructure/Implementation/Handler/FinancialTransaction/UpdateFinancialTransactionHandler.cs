using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Application.Abstracts.Interfaces.Repositories;
using Finlyze.Domain.Entities.FinancialTransactionEntity;
using Finlyze.Domain.Entities.SystemLogEntity;
using Finlyze.Domain.ValueObjects.Enums;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Infrastructure.Implementations.Interfaces.Handlers;

public class UpdateFinancialTransactionHandler : IUpdateFinancialTransactionHandler
{
    private readonly IFinancialTransactionQuery _tranQuery;
    private readonly IFinancialTransactionRepository _tranRepository;
    private readonly ISystemLogRepository _systemRepository;

    public UpdateFinancialTransactionHandler(IFinancialTransactionQuery tranQuery, IFinancialTransactionRepository tranRepository, ISystemLogRepository systemRepository)
    {
        _tranQuery = tranQuery;
        _tranRepository = tranRepository;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<FinancialTransaction>> Handle(UpdateFinancialTransactionCommand command)
    {
        try
        {
            var transaction = await _tranQuery.GetByIdAsync(command.Id);

            if (transaction is null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "FinancialTransaction", $"Não foi possível atualizar a transação: ID '{command.Id}' não encontrado."));
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
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Falha ao atualizar transação: ID '{transaction.Id}' do usuário '{transaction.UserAccountId}'."));
                return ResultHandler<FinancialTransaction>.Fail("Falha ao atualizar a transação.");
            }

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "FinancialTransaction", $"Transação atualizada com sucesso: ID '{transaction.Id}' vinculada ao usuário '{transaction.UserAccountId}'."));

            return ResultHandler<FinancialTransaction>.Ok("Transação atualizada com sucesso.", null);
        }

        catch (Exception ex) when (ex is DomainException or EnumException)
        {
            var errorMsg = ex.InnerException?.Message ?? ex.Message ?? "Erro de validação.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Validation, "FinancialTransaction", $"Erro de validação ao atualizar transação com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail(errorMsg);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Erro inesperado ao atualizar transação com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}