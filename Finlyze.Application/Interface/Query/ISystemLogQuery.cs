using Finlyze.Domain.Entities.SystemLogEntity;

namespace Finlyze.Application.Abstracts.Interfaces.Queries;

public interface ISystemLogQuery
{
    Task<SystemLog?> GetByIdAsync(int id);
    Task<IEnumerable<SystemLog>> GetByTypeAsync(int type);
    Task<IEnumerable<SystemLog>> GetByCreateAtAsync(DateTime create);
}