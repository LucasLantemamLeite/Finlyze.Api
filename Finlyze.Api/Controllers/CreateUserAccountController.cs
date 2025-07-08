using Finlyze.Api.Services.TokenHandler;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Dto;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserAccountDto user_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var command = new CreateUserAccountCommand(user_dto.Name, user_dto.Email, user_dto.Password, user_dto.PhoneNumber, user_dto.BirthDate);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(new { result.Message, TokenKey = JwtTokenHandler.GenerateToken(result.Data) });
        }

        catch
        {
            return StatusCode(500, new { Message = "Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}