using Finlyze.Domain.Entity;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;

namespace Finlyze.Application.Abstract.Interface.Handler;

public interface ICreateTransactionHandler
{
    Task<ResultHandler<Transaction>> Handle(CreateTransactionCommand command);
}