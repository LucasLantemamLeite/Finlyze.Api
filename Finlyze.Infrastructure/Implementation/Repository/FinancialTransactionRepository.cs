using System.Data;
using Dapper;
using Finlyze.Application.Abstracts.Interfaces.Repositories;
using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Infrastructure.Implementations.Interfaces.Repositories;

public class FinancialTransactionRepository : IFinancialTransactionRepository
{
    private readonly IDbConnection _connection;

    public FinancialTransactionRepository(IDbConnection connection) => _connection = connection;

    public async Task<int> CreateAsync(FinancialTransaction transaction)
    {
        var sql = @"INSERT INTO FinancialTransaction
        (Title, Description, Amount, TranType, CreateAt, TransactionUpdateAt, UserAccountId)
        VALUES
        (@Title, @Description, @Amount, @TranType, @CreateAt, @UpdateAt, @UserAccountId)
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

        var parameters = new
        {
            Title = transaction.Title.Value,
            Description = transaction.Description?.Value,
            Amount = transaction.Amount.Value,
            TranType = transaction.TranType.Value,
            CreateAt = transaction.CreateAt.Value,
            UpdateAt = transaction.UpdateAt.Value,
            transaction.UserAccountId
        };

        var id = await _connection.ExecuteScalarAsync<int>(sql, parameters);

        return id;
    }

    public async Task<int> DeleteAsync(FinancialTransaction transaction)
    {
        var sql = @"DELETE FROM FinancialTransaction WHERE Id = @Id";
        var parameters = new { transaction.Id };
        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateAsync(FinancialTransaction transaction)
    {
        var sql = @"UPDATE FinancialTransaction SET Title = @Title, Description = @Description, Amount = @Amount, TranType = @TranType, CreateAt = @CreateAt, UpdateAt = @UpdateAt  WHERE Id = @Id";

        var parameters = new
        {
            transaction.Id,
            Title = transaction.Title.Value,
            Description = transaction.Description?.Value,
            Amount = transaction.Amount.Value,
            TranType = transaction.TranType.Value,
            CreateAt = transaction.CreateAt.Value,
            UpdateAt = transaction.UpdateAt.Value,
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }
}