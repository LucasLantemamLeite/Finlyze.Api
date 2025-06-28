using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;

namespace Finlyze.Infrastructure.Implementation.Interfaces;

public class UserAccountQuery : IUserAccountQuery
{
    private readonly IDbConnection _connection;

    public UserAccountQuery(IDbConnection connection) => _connection = connection;

    const string SqlSelectBase = "SELECT Id, Name, Email, Password, PhoneNumber, BirthDate, CreateAt, Active, Role FROM UserAccount";

    public async Task<ResultPattern<UserAccountDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Id = @Id";
            var user = await _connection.QuerySingleOrDefaultAsync<UserAccountDto>(sql, new { Id = id });

            return ResultPattern<UserAccountDto>.Ok(null, user);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<UserAccountDto>.Fail($"Infrastructure -> UserAccountQuery -> GetByIdAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<UserAccountDto>> GetByLoginAsync(string login)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Login = @Login";
            var user = await _connection.QuerySingleOrDefaultAsync<UserAccountDto>(sql, new { Login = login });

            return ResultPattern<UserAccountDto>.Ok(null, user);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<UserAccountDto>.Fail($"Infrastructure -> UserAccountQuery -> GeyByLoginAsync: {errorMsg}");
        }
    }
}