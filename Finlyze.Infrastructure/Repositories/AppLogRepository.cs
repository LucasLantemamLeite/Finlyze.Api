using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces;

public class AppLogRepository : IAppLogRepository
{
    private readonly IDbConnection _connection;
    public AppLogRepository(IDbConnection connection) => _connection = connection;
    public async Task<int> CreateAsync(AppLog log)
    {
        var sql = @"INSERT INTO AppLog (Id, LogType, LogTitle, LogDescription, LogCreateAt)
        VALUES
        (@Id, @LogType, @LogTitle, @LogDescription, @LogCreateAt)";

        var parameters = new
        {
            Id = log.Id,
            LogType = log.LogType.Value,
            LogTitle = log.LogTitle.Value,
            LogDescription = log.LogDescription.Value,
            LogCreateAt = log.LogCreateAt.Value
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteAsync(AppLog log)
    {
        var sql = @"DELETE FROM AppLog WHERE Id = @Id";
        var parameters = new { Id = log.Id };
        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateAsync(AppLog log)
    {
        var sql = @"UPDATE AppLog SET LogType = @LogType, LogTitle = @LogTitle, LogDescription = @LogDescription, LogCreateAt = @LogCreateAt WHERE Id = @Id";

        var parameters = new
        {
            Id = log.Id,
            LogType = log.LogType.Value,
            LogTitle = log.LogTitle.Value,
            LogDescription = log.LogDescription.Value,
            LogCreateAt = log.LogCreateAt.Value
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }
}