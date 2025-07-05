using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface ICreateUserAccountHandler
{
    Task<ResultPattern<UserAccount>> Handle(CreateUserAccountCommand command);
}