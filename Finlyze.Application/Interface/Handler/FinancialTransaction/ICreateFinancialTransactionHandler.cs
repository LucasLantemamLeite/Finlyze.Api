using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Handlers;

public interface ICreateFinancialTransactionHandler
{
    Task<ResultHandler<FinancialTransaction>> Handle(CreateFinancialTransactionCommand command);
}