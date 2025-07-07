using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IAppLogQuery
{
    Task<AppLog?> GetByIdAsync(Guid id);
    Task<IEnumerable<AppLog?>> GeyByTypeAsync(int type);
    Task<IEnumerable<AppLog?>> GetByCreateAtAsync(DateTime create);
}