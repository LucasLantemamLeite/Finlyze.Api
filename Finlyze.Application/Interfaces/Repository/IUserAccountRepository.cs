using Finlyze.Domain.Entity;
using Finlyze.Application.Abstract.Interface.Result;

namespace Finlyze.Application.Abstract.Interface;

public interface IUserAccountRepository
{
    Task<ResultPattern<UserAccount>> CreateAsync(UserAccount user);

    Task<ResultPattern<UserAccount>> DeleteAsync(UserAccount user);

    Task<ResultPattern<UserAccount>> UpdateAsync(UserAccount user);
}