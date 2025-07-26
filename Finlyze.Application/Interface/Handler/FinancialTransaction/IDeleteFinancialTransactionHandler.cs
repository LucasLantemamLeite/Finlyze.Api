using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Handlers;

public interface IDeleteFinancialTransactionHandler
{
    Task<ResultHandler<FinancialTransaction>> Handle(DeleteFinancialTransactionCommand command);
}