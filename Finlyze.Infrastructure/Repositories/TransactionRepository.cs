using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces;

public class TransactionRepository : ITransactionRepository
{

    private readonly IDbConnection _connection;

    public TransactionRepository(IDbConnection connection) => _connection = connection;

    public async Task<ResultPattern<Transaction>> CreateAsync(Transaction transaction)
    {
        try
        {
            var sql = @"INSERT INTO Transaction
            (Id, TransactionTitle, TransactionDescription, Amount, TypeTransaction, TransactionCreateAt, TransactionUpdateAt, UserAccountId)
            VALUES
            (@Id, @TransactionTitle, @TransactionDescription, @Amount, @TypeTransaction, @TransactionCreateAt, @TransactionUpdateAt, @UserAccountId)";

            var parameters = new
            {
                Id = transaction.Id,
                TransactionTitle = transaction.TransactionTitle.Value,
                TransactionDescription = transaction.TransactionDescription?.Value,
                Amount = transaction.Amount.Value,
                TypeTransaction = transaction.TypeTransaction.Value,
                TransactionCreateAt = transaction.TransactionCreateAt.Value,
                TransactionUpdateAt = transaction.TransactionUpdateAt.Value,
                UserAccountId = transaction.UserAccountId
            };

            var rows = await _connection.ExecuteAsync(sql, parameters);

            return ResultPattern<Transaction>.Ok($"Transaction criado com sucesso, linhas afetada: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<Transaction>.Fail($"Infrastructure -> TransactionRepository -> CreateAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<Transaction>> DeleteAsync(Transaction transaction)
    {
        try
        {
            var sql = @"DELETE FROM Transaction WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, new { Id = transaction.Id });

            return ResultPattern<Transaction>.Ok($"Transaction deletado com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<Transaction>.Fail($"Infrastructure -> TransactionRepository -> DeleteAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<Transaction>> UpdateAsync(Transaction transaction)
    {
        try
        {
            var sql = @"UPDATE Transaction SET TransactionTitle = @TransactionTitle, TransactionDescription = @TransactionDescription, Amount = @Amount, TypeTransaction = @TypeTransaction, TransactionCreateAt = @TransactionCreateAt, TransactionUpdateAt = @TransactionUpdateAt  WHERE Id = @Id";

            var parameters = new
            {
                Id = transaction.Id,
                TransactionTitle = transaction.TransactionTitle.Value,
                TransactionDescription = transaction.TransactionDescription?.Value,
                Amount = transaction.Amount.Value,
                TypeTransaction = transaction.TypeTransaction.Value,
                TransactionCreateAt = transaction.TransactionCreateAt.Value,
                TransactionUpdateAt = transaction.TransactionUpdateAt.Value,
                UserAccountId = transaction.UserAccountId
            };

            var rows = await _connection.ExecuteAsync(sql, parameters);

            return ResultPattern<Transaction>.Ok($"Transaction atualizado com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<Transaction>.Fail($"Infrastructure -> TransactionRepository -> UpdateAsync: {errorMsg}");
        }
    }
}