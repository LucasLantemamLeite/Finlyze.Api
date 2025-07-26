using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Application.Abstracts.Interfaces.Repositories;
using Finlyze.Domain.Entities.FinancialTransactionEntity;
using Finlyze.Domain.Entities.SystemLogEntity;
using Finlyze.Domain.ValueObjects.Enums;

namespace Finlyze.Infrastructure.Implementations.Interfaces.Handlers;

public class DeleteFinancialTransactionHandler : IDeleteFinancialTransactionHandler
{
    private readonly IFinancialTransactionQuery _tranQuery;
    private readonly IFinancialTransactionRepository _tranRepository;
    private readonly ISystemLogRepository _systemRepository;

    public DeleteFinancialTransactionHandler(IFinancialTransactionQuery tranQuery, IFinancialTransactionRepository tranRepository, ISystemLogRepository systemRepository)
    {
        _tranQuery = tranQuery;
        _tranRepository = tranRepository;
        _systemRepository = systemRepository;
    }

    public async Task<ResultHandler<FinancialTransaction>> Handle(DeleteFinancialTransactionCommand command)
    {
        try
        {
            var transaction = await _tranQuery.GetByIdAsync(command.Id);

            if (transaction is null)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Warning, "FinancialTransaction", $"Não foi possível deletar a transação: ID '{command.Id}' não encontrado."));
                return ResultHandler<FinancialTransaction>.Fail("Transação com esse ID não foi encontrada.");
            }

            var rows = await _tranRepository.DeleteAsync(transaction);

            if (rows == 0)
            {
                await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Falha ao deletar transação: ID '{transaction.Id}' do usuário '{transaction.UserAccountId}'."));
                return ResultHandler<FinancialTransaction>.Fail("Falha ao deletar transação.");
            }

            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Info, "FinancialTransaction", $"Transação deletada com sucesso: ID '{transaction.Id}' vinculada ao usuário '{transaction.UserAccountId}'."));

            return ResultHandler<FinancialTransaction>.Ok("Transação deletada com sucesso.", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _systemRepository.CreateAsync(new SystemLog((int)ELog.Error, "FinancialTransaction", $"Erro inesperado ao deletar transação com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<FinancialTransaction>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}