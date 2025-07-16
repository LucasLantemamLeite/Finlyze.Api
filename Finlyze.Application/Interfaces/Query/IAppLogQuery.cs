using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IAppLogQuery
{
    Task<SystemLog?> GetByIdAsync(int id);
    Task<IEnumerable<SystemLog>> GetByTypeAsync(int type);
    Task<IEnumerable<SystemLog>> GetByCreateAtAsync(DateTime create);
}