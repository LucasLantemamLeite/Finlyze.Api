using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;
using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Handler;

public class DeleteTransactionHandler : IDeleteTransactionHandler
{
    private readonly ITransactionQuery _tranQuery;
    private readonly ITransactionRepository _tranRepository;
    private readonly IAppLogRepository _appRepository;

    public DeleteTransactionHandler(ITransactionQuery tranQuery, ITransactionRepository tranRepository, IAppLogRepository appRepository)
    {
        _tranQuery = tranQuery;
        _tranRepository = tranRepository;
        _appRepository = appRepository;
    }

    public async Task<ResultHandler<Transaction>> Handle(DeleteTransactionCommand command)
    {
        try
        {
            var transaction = await _tranQuery.GetByIdAsync(command.Id);

            if (transaction is null)
                return ResultHandler<Transaction>.Fail("Nenhuma Transaction encontrada com esse Id.");

            var rows = await _tranRepository.DeleteAsync(transaction);

            if (rows == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Delete", "Não foi possível deletar essa Transaction"));
                return ResultHandler<Transaction>.Fail("Falha ao deletar Transaction.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Delete", $"Transaction com Id: {transaction.Id} deletado com sucesso"));

            return ResultHandler<Transaction>.Ok("Transaction deletada com sucesso.", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro Desconhecido";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Delete", $"Ocorreu um erro ao deletar uma transaction do usuário: {errorMsg}"));
            return ResultHandler<Transaction>.Fail($"Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}