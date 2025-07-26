using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers.Result;
using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Handlers;

public interface ILoginUserAccountHandler
{
    Task<ResultHandler<UserAccount>> Handle(LoginUserAccountCommand command);
}