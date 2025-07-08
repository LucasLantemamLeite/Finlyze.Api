using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;
using Finlyze.Application.Entity.Raw;
using Finlyze.Application.Entity.Raw.Convert;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Query;

public class TransactionQuery : ITransactionQuery
{
    private readonly IDbConnection _connection;
    public TransactionQuery(IDbConnection connection) => _connection = connection;
    private const string SqlSelectBase = "SELECT Id, TransactionTitle, TransactionDescription, Amount, TypeTransaction, TransactionCreateAt, TransactionUpdateAt, UserAccountId FROM Transaction";

    public async Task<IEnumerable<Transaction>> GetByAmountAsync(decimal amount)
    {
        var sql = $"{SqlSelectBase} WHERE Amount = @Amount";
        var parameters = new { Amount = amount };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }

    public async Task<IEnumerable<Transaction>> GetByCreateAtAsync(DateTime create_date)
    {
        var sql = $"{SqlSelectBase} WHERE TransactionCreateAt = @TransactionCreateAt";
        var parameters = new { TransactionCreateAt = create_date };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        var sql = $"{SqlSelectBase} WHERE Id = @Id";
        var parameters = new { Id = id };
        var raw = await _connection.QuerySingleOrDefaultAsync<TransactionRaw>(sql, parameters);

        return raw == null ? null : raw.ToSingleTransaction();
    }

    public async Task<IEnumerable<Transaction>> GetByTitleAsync(string title)
    {
        var sql = $"{SqlSelectBase} WHERE TransactionTitle = @TransactionTitle";
        var parameters = new { TransactionTitle = title };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }

    public async Task<IEnumerable<Transaction>> GetByTypeAsync(int type)
    {
        var sql = $"{SqlSelectBase} WHERE TypeTransaction = @TypeTransaction";
        var parameters = new { TypeTransaction = type };
        var raw = await _connection.QueryAsync<TransactionRaw>(sql, parameters);

        return raw.ToEnumerableTransaction();
    }
}