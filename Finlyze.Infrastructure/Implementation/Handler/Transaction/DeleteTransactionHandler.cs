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
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Warning, "Transaction", $"Não foi possível deletar a transação: ID '{command.Id}' não encontrado."));
                return ResultHandler<Transaction>.Fail("Transação com esse ID não foi encontrada.");
            }

            var rows = await _tranRepository.DeleteAsync(transaction);

            if (rows == 0)
            {
                await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Transaction", $"Falha ao deletar transação: ID '{transaction.Id}' do usuário '{transaction.UserAccountId}'."));
                return ResultHandler<Transaction>.Fail("Falha ao deletar transação.");
            }

            await _appRepository.CreateAsync(new AppLog((int)ELog.Info, "Transaction", $"Transação deletada com sucesso: ID '{transaction.Id}' vinculada ao usuário '{transaction.UserAccountId}'."));

            return ResultHandler<Transaction>.Ok("Transação deletada com sucesso.", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            await _appRepository.CreateAsync(new AppLog((int)ELog.Error, "Transaction", $"Erro inesperado ao deletar transação com ID '{command.Id}': {errorMsg}"));
            return ResultHandler<Transaction>.Fail("Ocorreu um erro interno no servidor. Tente novamente mais tarde.");
        }
    }
}