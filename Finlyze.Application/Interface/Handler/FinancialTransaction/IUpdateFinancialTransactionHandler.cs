using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Handlers;

public interface IUpdateFinancialTransactionHandler
{
    Task<ResultHandler<FinancialTransaction>> Handle(UpdateFinancialTransactionCommand command);
}