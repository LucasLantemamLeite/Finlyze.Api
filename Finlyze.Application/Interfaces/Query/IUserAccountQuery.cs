using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IUserAccountQuery
{
    Task<ResultPattern<UserAccount>> GetByIdAsync(Guid id);
    Task<ResultPattern<UserAccount>> GetByEmailAsync(string email);
    Task<ResultPattern<UserAccount>> GetByPhoneNumberAsync(string phone);
}