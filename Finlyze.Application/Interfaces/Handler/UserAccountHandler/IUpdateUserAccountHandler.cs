using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IUpdateUserAccountHandler
{
    Task<ResultHandler<UserAccount>> Handle(UpdateUserAccountCommand command);
}