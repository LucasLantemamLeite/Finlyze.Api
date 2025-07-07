using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controller.UserAccount;

[ApiController]
[Route("api/v1")]
[Tags("Users")]
public class CreateAdminUserAccountController : ControllerBase
{
    private readonly ICreateAdminUserAccountHandler _handler;
    public CreateAdminUserAccountController(ICreateAdminUserAccountHandler handler) => _handler = handler;

    public async Task<IActionResult> CreateAsync([FromBody] CreateUserAccountDto user_dto)
    {
        try
        {
            var command = new CreateUserAccountCommand(user_dto.Name, user_dto.Email, user_dto.Password, user_dto.PhoneNumber, user_dto.BirthDate);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            return BadRequest($"Controller -> CreateAdminUserAccountController -> CreateAsync: {errorMsg}");
        }
    }
}