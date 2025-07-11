using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface ILoginUserAccountHandler
{
    Task<ResultHandler<UserAccount>> Handle(LoginUserAccountCommand command);
}