using System.Transactions;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;

namespace Finlyze.Application.Abstract.Interface.Handler;

public interface DeleteUserAccountHandler
{
    Task<ResultHandler<Transaction>> Handle(DeleteTransactionCommand command);
}