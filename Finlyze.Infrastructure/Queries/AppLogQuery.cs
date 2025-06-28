using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;

namespace Finlyze.Infrastructure.Implementation.Interfaces;

public class AppLogQuery : IAppLogQuery
{

    private readonly IDbConnection _connection;

    public AppLogQuery(IDbConnection connection) => _connection = connection;

    const string SqlSelectBase = "SELECT Id, LogType, LogTitle, LogDescription, LogCreateAt FROM AppLog";

    public async Task<ResultPattern<IEnumerable<AppLogDto>>> GetByCreateAtAsync(DateTime create)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE LogCreateAt = @LogCreateAt";
            var logs = await _connection.QueryAsync<AppLogDto>(sql, new { LogCreateAt = create });

            return ResultPattern<IEnumerable<AppLogDto>>.Ok(null, logs);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<IEnumerable<AppLogDto>>.Fail($"Infrastructure -> AppLogQuery -> GetByCreateAtAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<AppLogDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Id = @Id";
            var log = await _connection.QuerySingleOrDefaultAsync<AppLogDto>(sql, new { Id = id });

            return ResultPattern<AppLogDto>.Ok(null, log);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<AppLogDto>.Fail($"Infrastructure -> AppLogQuery -> GetByIdAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<IEnumerable<AppLogDto>>> GeyByTypeAsync(int type)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE LogType = @LogType";
            var logs = await _connection.QueryAsync<AppLogDto>(sql, new { LogType = type });

            return ResultPattern<IEnumerable<AppLogDto>>.Ok(null, logs);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<IEnumerable<AppLogDto>>.Fail($"Infrastructure -> AppLogQuery -> GeyByTypeAsync: {errorMsg}");
        }
    }
}