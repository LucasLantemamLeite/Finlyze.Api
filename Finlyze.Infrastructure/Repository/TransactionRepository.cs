using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces;

public class TransactionRepository : ITransactionRepository
{
    private readonly IDbConnection _connection;
    public TransactionRepository(IDbConnection connection) => _connection = connection;
    public async Task<int> CreateAsync(Transaction transaction)
    {

        var sql = @"INSERT INTO [Transaction]
        (TransactionTitle, TransactionDescription, Amount, TypeTransaction, TransactionCreateAt, TransactionUpdateAt, UserAccountId)
        VALUES
        (@TransactionTitle, @TransactionDescription, @Amount, @TypeTransaction, @TransactionCreateAt, @TransactionUpdateAt, @UserAccountId)";

        var parameters = new
        {
            TransactionTitle = transaction.TransactionTitle.Value,
            TransactionDescription = transaction.TransactionDescription?.Value,
            Amount = transaction.Amount.Value,
            TypeTransaction = transaction.TypeTransaction.Value,
            TransactionCreateAt = transaction.TransactionCreateAt.Value,
            TransactionUpdateAt = transaction.TransactionUpdateAt.Value,
            UserAccountId = transaction.UserAccountId
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteAsync(Transaction transaction)
    {
        var sql = @"DELETE FROM [Transaction] WHERE Id = @Id";
        var parameters = new { Id = transaction.Id };
        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateAsync(Transaction transaction)
    {
        var sql = @"UPDATE [Transaction] SET TransactionTitle = @TransactionTitle, TransactionDescription = @TransactionDescription, Amount = @Amount, TypeTransaction = @TypeTransaction, TransactionCreateAt = @TransactionCreateAt, TransactionUpdateAt = @TransactionUpdateAt  WHERE Id = @Id";

        var parameters = new
        {
            Id = transaction.Id,
            TransactionTitle = transaction.TransactionTitle.Value,
            TransactionDescription = transaction.TransactionDescription?.Value,
            Amount = transaction.Amount.Value,
            TypeTransaction = transaction.TypeTransaction.Value,
            TransactionCreateAt = transaction.TransactionCreateAt.Value,
            TransactionUpdateAt = transaction.TransactionUpdateAt.Value,
            transaction.UserAccountId
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }
}