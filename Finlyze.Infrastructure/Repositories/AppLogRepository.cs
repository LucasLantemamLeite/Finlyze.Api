using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces;

public class AppLogRepository : IAppLogRepository
{
    private readonly IDbConnection _connection;
    public AppLogRepository(IDbConnection connection) => _connection = connection;
    public async Task<ResultPattern<AppLog>> CreateAsync(AppLog log)
    {
        try
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

            var rows = await _connection.ExecuteAsync(sql, parameters);

            return ResultPattern<AppLog>.Ok($"Log criado com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<AppLog>.Fail($"Infrastructure -> AppLogRepository -> CreateAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<AppLog>> DeleteAsync(AppLog log)
    {
        try
        {
            var sql = @"DELETE FROM AppLog WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, new { Id = log.Id });

            return ResultPattern<AppLog>.Ok($"Log deletado com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<AppLog>.Fail($"Infrastructure -> AppLogRepository -> DeleteAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<AppLog>> UpdateAsync(AppLog log)
    {
        try
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

            var rows = await _connection.ExecuteAsync(sql, parameters);

            return ResultPattern<AppLog>.Ok($"Log atualizado com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<AppLog>.Fail($"Infrastructure -> AppLogRepository -> UpdateAsync: {errorMsg}");
        }
    }
}