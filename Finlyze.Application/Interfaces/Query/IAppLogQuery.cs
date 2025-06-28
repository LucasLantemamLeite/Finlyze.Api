using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;
using Finlyze.Domain.Entity;

namespace Finlyze.Application.Abstract.Interface;

public interface IAppLogQuery
{
    Task<ResultPattern<AppLogDto>> GetByIdAsync(Guid id);
    Task<ResultPattern<IEnumerable<AppLogDto>>> GeyByTypeAsync(int type);
    Task<ResultPattern<IEnumerable<AppLogDto>>> GetByCreateAtAsync(DateTime create);
}