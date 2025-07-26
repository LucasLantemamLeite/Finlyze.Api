using Finlyze.Domain.Entities.UserAccountEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Repositories;

public interface IUserAccountRepository
{
    Task<int> CreateAsync(UserAccount user);
    Task<int> DeleteAsync(UserAccount user);
    Task<int> UpdateAsync(UserAccount user);
}