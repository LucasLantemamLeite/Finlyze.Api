using Finlyze.Domain.Entities.SystemLogEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Repositories;

public interface ISystemLogRepository
{
    Task<int> CreateAsync(SystemLog log);
    Task<int> DeleteAsync(SystemLog log);
    Task<int> UpdateAsync(SystemLog log);
}