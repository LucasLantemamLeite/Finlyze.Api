using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IUserAccountRepository
{
    Task<int?> CreateAsync(UserAccount user);
    Task<int?> DeleteAsync(UserAccount user);
    Task<int?> UpdateAsync(UserAccount user);
}