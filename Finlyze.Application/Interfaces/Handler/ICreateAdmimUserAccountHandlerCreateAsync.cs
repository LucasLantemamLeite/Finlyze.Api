using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface ICreateAdminUserAccountHandler
{
    Task<ResultPattern<UserAccount>> Handle(CreateUserAccountCommand command);
}