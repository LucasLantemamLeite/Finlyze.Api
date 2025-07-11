using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;
using Finlyze.Application.Authentication.Hasher;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Repository;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly IDbConnection _connection;
    public UserAccountRepository(IDbConnection connection) => _connection = connection;
    public async Task<int> CreateAsync(UserAccount user)
    {
        var sql = @"
            INSERT INTO UserAccount 
            (Id, Name, Email, Password, PhoneNumber, BirthDate, CreateAt, Active, Role)
            VALUES
            (@Id, @Name, @Email, @Password, @PhoneNumber, @BirthDate, @CreateAt, @Active, @Role)";

        var parameters = new
        {
            Id = user.Id,
            Name = user.Name.Value,
            Email = user.Email.Value,
            Password = user.Password.Value.GenerateHash(),
            PhoneNumber = user.PhoneNumber.Value,
            BirthDate = user.BirthDate.Value.ToDateTime(TimeOnly.MinValue),
            CreateAt = user.CreateAt.Value,
            Active = user.Active.Value,
            Role = user.Role.Value
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteAsync(UserAccount user)
    {
        var sql = @"DELETE FROM UserAccount WHERE Id = @Id";
        var parameters = new { Id = user.Id };
        return await _connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> UpdateAsync(UserAccount user)
    {
        var sql = @"UPDATE UserAccount SET Name = @Name, Email = @Email, Password = @Password, PhoneNumber = @PhoneNumber WHERE Id = @Id";

        var parameters = new
        {
            Id = user.Id,
            Name = user.Name.Value,
            Email = user.Email.Value,
            Password = user.Password.Value.GenerateHash(),
            PhoneNumber = user.PhoneNumber.Value
        };

        return await _connection.ExecuteAsync(sql, parameters);
    }
}