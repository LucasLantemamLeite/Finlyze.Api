using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;
using Finlyze.Application.Entity.Raw;
using Finlyze.Application.Entity.Raw.Convert;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Query;

public class FinancialTransactionQuery : IFinancialTransactionQuery
{
    private readonly IDbConnection _connection;

    public FinancialTransactionQuery(IDbConnection connection) => _connection = connection;
    private const string SqlSelectBase = "SELECT Id, TransactionTitle, TransactionDescription, Amount, TypeTransaction, TransactionCreateAt, TransactionUpdateAt, UserAccountId FROM FinancialTransaction";

    public async Task<IEnumerable<FinancialTransaction>> GetByAmountAsync(decimal amount)
    {
        var sql = $"{SqlSelectBase} WHERE Amount = @Amount";
        var parameters = new { Amount = amount };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByCreateAtAsync(DateTime create_date)
    {
        var sql = $"{SqlSelectBase} WHERE CreateAt = @CreateAt";
        var parameters = new { CreateAt = create_date };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }

    public async Task<FinancialTransaction?> GetByIdAsync(int id)
    {
        var sql = $"{SqlSelectBase} WHERE Id = @Id";
        var parameters = new { Id = id };
        var raw = await _connection.QuerySingleOrDefaultAsync<TransactionRaw>(sql, parameters);

        return raw == null ? null : raw.ToSingleTransaction();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByTitleAsync(string title)
    {
        var sql = $"{SqlSelectBase} WHERE Title = @Title";
        var parameters = new { Title = title };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByTypeAsync(int type)
    {
        var sql = $"{SqlSelectBase} WHERE TranType = @TranType";
        var parameters = new { TranType = type };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }
}