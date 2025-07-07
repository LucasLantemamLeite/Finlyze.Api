using Finlyze.Api.Services.TokenHandler;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controller.UserAccount;

[ApiController]
[Route("api/v1")]
[Tags("Users")]
public class CreateUserAccountController : ControllerBase
{
    private readonly ICreateUserAccountHandler _handler;
    public CreateUserAccountController(ICreateUserAccountHandler handler) => _handler = handler;

    [HttpPost("users")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserAccountDto user_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateUserAccountCommand(user_dto.Name, user_dto.Email, user_dto.Password, user_dto.PhoneNumber, user_dto.BirthDate);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(result.Message);

            return StatusCode(200, new { result.Message, TokenKey = JwtTokenHandler.GenerateToken(result.Data) });
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            return BadRequest($"Controller -> CreateUserAccountController -> CreateAsync: {errorMsg}");
        }
    }
}