using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IAppLogRepository
{
    Task<ResultPattern<AppLog>> CreateAsync(AppLog log);
    Task<ResultPattern<AppLog>> DeleteAsync(AppLog log);
    Task<ResultPattern<AppLog>> UpdateAsync(AppLog log);
}