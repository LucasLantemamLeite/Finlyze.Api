using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Handlers;

public interface IDeleteAccountHandler
{
    Task<ResultHandler<UserAccount>> Handle(DeleteUserAccountCommand command);
}