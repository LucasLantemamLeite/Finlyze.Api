using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface ICreateUserAccountHandler
{
    Task<ResultHandler<UserAccount>> Handle(CreateUserAccountCommand command);
}