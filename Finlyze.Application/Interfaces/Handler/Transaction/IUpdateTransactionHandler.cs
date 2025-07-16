using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface.Handler;

public interface IUpdateTransactionHandler
{
    Task<ResultHandler<FinancialTransaction>> Handle(UpdateTransactionCommand command);
}