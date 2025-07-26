using System.Data;
using Dapper;
using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Domain.Entities.SystemLogEntity;
using Finlyze.Application.Entities.Raws.Converts;
using Finlyze.Application.Entities.Raws;

namespace Finlyze.Infrastructure.Implementations.Interfaces.Queries;

public class SystemLogQuery : ISystemLogQuery
{
    private readonly IDbConnection _connection;

    public SystemLogQuery(IDbConnection connection) => _connection = connection;

    private const string SqlSelectBase = "SELECT Id, LogType, LogTitle, LogDescription, LogCreateAt FROM SystemLog";

    public async Task<IEnumerable<SystemLog>> GetByCreateAtAsync(DateTime create)
    {
        var sql = $"{SqlSelectBase} WHERE CreateAt = @CreateAt";
        var raw = await _connection.QueryAsync<SystemLogRaw>(sql, new { CreateAt = create });

        return raw.ToEnumerableAppLog();
    }

    public async Task<SystemLog?> GetByIdAsync(int id)
    {
        var sql = $"{SqlSelectBase} WHERE Id = @Id";
        var raw = await _connection.QuerySingleOrDefaultAsync<SystemLogRaw>(sql, new { Id = id });

        return raw == null ? null : raw.ToSingleAppLog();
    }

    public async Task<IEnumerable<SystemLog>> GetByTypeAsync(int type)
    {
        var sql = $"{SqlSelectBase} WHERE LogType = @LogType";
        var raw = await _connection.QueryAsync<SystemLogRaw>(sql, new { LogType = type });

        return raw.ToEnumerableAppLog();
    }
}