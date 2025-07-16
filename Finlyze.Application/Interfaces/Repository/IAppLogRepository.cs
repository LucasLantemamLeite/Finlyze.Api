using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IAppLogRepository
{
    Task<int> CreateAsync(SystemLog log);
    Task<int> DeleteAsync(SystemLog log);
    Task<int> UpdateAsync(SystemLog log);
}