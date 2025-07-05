using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IDeleteAccountHandler
{
    Task<ResultPattern<UserAccount>> Handle(DeleteUserAccountCommand command);
}