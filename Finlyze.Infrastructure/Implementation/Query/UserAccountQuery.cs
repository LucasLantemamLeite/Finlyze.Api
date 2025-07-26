using System.Data;
using Dapper;
using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Domain.Entities.UserAccountEntity;
using Finlyze.Application.Entities.Raws.Converts;
using Finlyze.Application.Entities.Raws;

namespace Finlyze.Infrastructure.Implementations.Interfaces.Queries;

public class UserAccountQuery : IUserAccountQuery
{
    private readonly IDbConnection _connection;

    public UserAccountQuery(IDbConnection connection) => _connection = connection;

    private const string SqlSelectBase = "SELECT Id, Name, Email, Password, PhoneNumber, BirthDate, CreateAt, Active, Role FROM UserAccount";

    public async Task<UserAccount?> GetByIdAsync(Guid id)
    {
        var sql = $"{SqlSelectBase} WHERE Id = @Id";
        var parameters = new { Id = id };
        var raw = await _connection.QuerySingleOrDefaultAsync<UserAccountRaw>(sql, parameters);

        return raw == null ? null : raw.ToUserAccount();
    }

    public async Task<UserAccount?> GetByEmailAsync(string email)
    {
        var sql = $"{SqlSelectBase} WHERE Email = @Email";
        var parameters = new { Email = email };
        var raw = await _connection.QuerySingleOrDefaultAsync<UserAccountRaw>(sql, parameters);

        return raw == null ? null : raw.ToUserAccount();
    }

    public async Task<UserAccount?> GetByPhoneNumberAsync(string phone)
    {
        var sql = $"{SqlSelectBase} WHERE PhoneNumber = @PhoneNumber";
        var parameters = new { PhoneNumber = phone };
        var raw = await _connection.QuerySingleOrDefaultAsync<UserAccountRaw>(sql, parameters);

        return raw == null ? null : raw.ToUserAccount();
    }
}