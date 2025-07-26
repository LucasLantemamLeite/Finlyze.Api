using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Handlers;

public interface ICreateUserAccountHandler
{
    Task<ResultHandler<UserAccount>> Handle(CreateUserAccountCommand command);
}