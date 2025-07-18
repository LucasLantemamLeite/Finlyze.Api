using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Repository;

public class SystemLogRepository : ISystemLogRepository
{
    private readonly IDbConnection _connection;

    public SystemLogRepository(IDbConnection connection) => _connection = connection;

    public async Task<int> CreateAsync(SystemLog log)
    {
        var sql = @"INSERT INTO SystemLog (LogType, Title, Description, CreateAt)
        VALUES
        (@LogType, @Title, @Description, @CreateAt)";

        var parameters = new
        {
            LogType = log.LogType.Value,
            Title = log.Title.Value,
            Description = log.Description.Value,
            CreateAt = log.CreateAt.Value
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteAsync(SystemLog log)
    {
        var sql = @"DELETE FROM SystemLog WHERE Id = @Id";
        var parameters = new { log.Id };
        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateAsync(SystemLog log)
    {
        var sql = @"UPDATE AppLog SET LogType = @LogType, Title = @Title, Description = @Description, CreateAt = @CreateAt WHERE Id = @Id";

        var parameters = new
        {
            log.Id,
            LogType = log.LogType.Value,
            Title = log.Title.Value,
            Description = log.Description.Value,
            CreateAt = log.CreateAt.Value
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }
}