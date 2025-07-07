using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IAppLogRepository
{
    Task<int> CreateAsync(AppLog log);
    Task<int> DeleteAsync(AppLog log);
    Task<int> UpdateAsync(AppLog log);
}