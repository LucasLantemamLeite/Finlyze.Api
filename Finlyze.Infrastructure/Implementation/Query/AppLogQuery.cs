using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Entity.Raw;
using Finlyze.Application.Entity.Raw.Convert;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Query;

public class AppLogQuery : IAppLogQuery
{
    private readonly IDbConnection _connection;

    public AppLogQuery(IDbConnection connection) => _connection = connection;

    private const string SqlSelectBase = "SELECT Id, LogType, LogTitle, LogDescription, LogCreateAt FROM SystemLog";

    public async Task<IEnumerable<SystemLog>> GetByCreateAtAsync(DateTime create)
    {
        var sql = $"{SqlSelectBase} WHERE CreateAt = @CreateAt";
        var raw = await _connection.QueryAsync<AppLogRaw>(sql, new { CreateAt = create });

        return raw.ToEnumerableAppLog();
    }

    public async Task<SystemLog?> GetByIdAsync(int id)
    {
        var sql = $"{SqlSelectBase} WHERE Id = @Id";
        var raw = await _connection.QuerySingleOrDefaultAsync<AppLogRaw>(sql, new { Id = id });

        return raw == null ? null : raw.ToSingleAppLog();
    }

    public async Task<IEnumerable<SystemLog>> GetByTypeAsync(int type)
    {
        var sql = $"{SqlSelectBase} WHERE LogType = @LogType";
        var raw = await _connection.QueryAsync<AppLogRaw>(sql, new { LogType = type });

        return raw.ToEnumerableAppLog();
    }
}