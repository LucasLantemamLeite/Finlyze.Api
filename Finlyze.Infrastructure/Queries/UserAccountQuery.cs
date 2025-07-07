using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Query;

public class UserAccountQuery : IUserAccountQuery
{
    private readonly IDbConnection _connection;

    public UserAccountQuery(IDbConnection connection) => _connection = connection;

    const string SqlSelectBase = "SELECT Id, Name, Email, Password, PhoneNumber, BirthDate, CreateAt, Active, Role FROM UserAccount";

    public async Task<ResultPattern<UserAccount>> GetByIdAsync(Guid id)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Id = @Id";
            var query = await _connection.QuerySingleOrDefaultAsync<UserAccountDto>(sql, new { Id = id });

            var userAccount = new UserAccount(query.Id, query.Name, query.Email, query.Password, query.PhoneNumber, DateOnly.FromDateTime(query.BirthDate), query.CreateAt, query.Active, query.Role);

            return ResultPattern<UserAccount>.Ok(null, userAccount);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<UserAccount>.Fail($"Infrastructure -> UserAccountQuery -> GetByIdAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<UserAccount>> GetByEmailAsync(string email)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE Email = @Email";
            var query = await _connection.QuerySingleOrDefaultAsync<UserAccountDto>(sql, new { Email = email });

            var userAccount = new UserAccount(query.Id, query.Name, query.Email, query.Password, query.PhoneNumber, DateOnly.FromDateTime(query.BirthDate), query.CreateAt, query.Active, query.Role);

            return ResultPattern<UserAccount>.Ok(null, userAccount);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<UserAccount>.Fail($"Infrastructure -> UserAccountQuery -> GeyByLoginAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<UserAccount>> GetByPhoneNumberAsync(string phone)
    {
        try
        {
            var sql = $"{SqlSelectBase} WHERE PhoneNumber = @PhoneNumber";
            var query = await _connection.QuerySingleOrDefaultAsync<UserAccountDto>(sql, new { PhoneNumber = phone });

            var userAccount = new UserAccount(query.Id, query.Name, query.Email, query.Password, query.PhoneNumber, DateOnly.FromDateTime(query.BirthDate), query.CreateAt, query.Active, query.Role);

            return ResultPattern<UserAccount>.Ok(null, userAccount);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido.";
            return ResultPattern<UserAccount>.Fail($"Infrastructure -> UserAccountQuery -> GetByPhoneNumberAsync: {errorMsg}");
        }
    }
}