using System.Data;
using Dapper;
using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Domain.Entity;

namespace Finlyze.Infrastructure.Implementation.Interfaces.Repository;

public class UserAccountRepository : IUserAccountRepository
{

    private readonly IDbConnection _connection;

    public UserAccountRepository(IDbConnection connection) => _connection = connection;

    public async Task<ResultPattern<UserAccount>> CreateAsync(UserAccount user)
    {
        try
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
                Password = user.Password.Value,
                PhoneNumber = user.PhoneNumber.Value,
                BirthDate = user.BirthDate.Value,
                CreateAt = user.CreateAt.Value,
                Active = user.Active.Value,
                Role = user.Role.Value
            };

            var rows = await _connection.ExecuteAsync(sql, parameters);

            return ResultPattern<UserAccount>.Ok($"Conta criada com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada de Repository.";
            return ResultPattern<UserAccount>.Fail($"Infrastructure -> UserAccountRepository -> CreateAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<UserAccount>> DeleteAsync(UserAccount user)
    {
        try
        {
            var sql = @"DELETE FROM UserAccount WHERE Id = @Id";
            var rows = await _connection.ExecuteReaderAsync(sql, new { Id = user.Id });

            return ResultPattern<UserAccount>.Ok($"Conta deletada com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada de Repository.";
            return ResultPattern<UserAccount>.Fail($"Infrastructure -> UserAccountRepository -> DeleteAsync: {errorMsg}");
        }
    }

    public async Task<ResultPattern<UserAccount>> UpdateAsync(UserAccount user)
    {
        try
        {
            var sql = @"UPDATE UserAccount SET Name = @Name, Email = @Email, Password = @Password, PhoneNumber = @PhoneNumber, Active = @Active, Role = @Role WHERE Id = @Id";
            var rows = await _connection.ExecuteAsync(sql, user);

            return ResultPattern<UserAccount>.Ok($"Conta atualizado com sucesso, linhas afetadas: {rows}", null);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido na camada de Repository.";
            return ResultPattern<UserAccount>.Fail($"Infrastructure -> UserAccountRepository -> UpdateAsync: {errorMsg}");
        }
    }
}