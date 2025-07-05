using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Query;

public class TransactionQuery : ITransactionQuery
{
    private readonly IDbConnection _connection;

    public TransactionQuery(IDbConnection connection) => _connection = connection;

    const string SqlSelectBase = "SELECT Id, TransactionTitle, TransactionDescription, Amount, TypeTransaction, TransactionCreateAt, TransactionUpdateAt, UserAccountId FROM Transaction";

    public async Task<ResultPattern<IEnumerable<TransactionDto>>> GetByAmountAsync(decimal amount)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Amount = @Amount";
            var transactions = await _connection.QueryAsync<TransactionDto>(sql, new { Amount = amount });

            return ResultPattern<IEnumerable<TransactionDto>>.Ok(null, transactions);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<IEnumerable<TransactionDto>>.Fail($"Infrastructure -> TransactionQuery -> GetByAmountAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<IEnumerable<TransactionDto>>> GetByCreateAtAsync(DateTime create_date)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE TransactionCreateAt = @TransactionCreateAt";
            var transactions = await _connection.QueryAsync<TransactionDto>(sql, new { TransactionCreateAt = create_date });

            return ResultPattern<IEnumerable<TransactionDto>>.Ok(null, transactions);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<IEnumerable<TransactionDto>>.Fail($"Infrastructure -> TransactionQuery -> GetByCreateAtAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<TransactionDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Id = @Id";
            var transaction = await _connection.QuerySingleOrDefaultAsync<TransactionDto>(sql, new { Id = id });

            return ResultPattern<TransactionDto>.Ok(null, transaction);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<TransactionDto>.Fail($"Infrastructure -> TransactionQuery -> GetByIdAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<IEnumerable<TransactionDto>>> GetByTitleAsync(string title)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE TransactionTitle = @TransactionTitle";
            var transactions = await _connection.QueryAsync<TransactionDto>(sql, new { TransactionTitle = title });

            return ResultPattern<IEnumerable<TransactionDto>>.Ok(null, transactions);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<IEnumerable<TransactionDto>>.Fail($"Infrastructure -> TransactionQuery -> GetByTitleAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<IEnumerable<TransactionDto>>> GetByTypeAsync(int type)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE TypeTransaction = @TypeTransaction";
            var transactions = await _connection.QueryAsync<TransactionDto>(sql, new { TypeTransaction = type });

            return ResultPattern<IEnumerable<TransactionDto>>.Ok(null, transactions);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<IEnumerable<TransactionDto>>.Fail($"Infrastructure -> TransactionQuery -> GetByTypeAsync: {errorMsg}");
        }
    }
}