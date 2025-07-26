using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Queries;

public interface IUserAccountQuery
{
    Task<UserAccount?> GetByIdAsync(Guid id);
    Task<UserAccount?> GetByEmailAsync(string email);
    Task<UserAccount?> GetByPhoneNumberAsync(string phone);
}