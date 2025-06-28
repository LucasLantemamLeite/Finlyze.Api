using Finlyze.Application.Abstract.Interface.Result;
using Finlyze.Application.Dto;

namespace Finlyze.Application.Abstract.Interface;

public interface IUserAccountQuery
{
    Task<ResultPattern<UserAccountDto>> GetByIdAsync(Guid id);
    Task<ResultPattern<UserAccountDto>> GetByEmailAsync(string email);
}