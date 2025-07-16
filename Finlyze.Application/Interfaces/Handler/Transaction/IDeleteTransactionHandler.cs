using Finlyze.Domain.Entity;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;

namespace Finlyze.Application.Abstract.Interface.Handler;

public interface IDeleteTransactionHandler
{
    Task<ResultHandler<FinancialTransaction>> Handle(DeleteTransactionCommand command);
}