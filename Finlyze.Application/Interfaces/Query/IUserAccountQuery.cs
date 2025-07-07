using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IUserAccountQuery
{
    Task<UserAccount?> GetByIdAsync(Guid id);
    Task<UserAccount?> GetByEmailAsync(string email);
    Task<UserAccount?> GetByPhoneNumberAsync(string phone);
}