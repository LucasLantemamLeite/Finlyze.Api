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
public class LoginUserAccountController : ControllerBase
{
    private readonly ILoginUserAccountHandler _handler;
    public LoginUserAccountController(ILoginUserAccountHandler handler) => _handler = handler;

    [AllowAnonymous]
    [HttpPost("users-login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserAccountDto login_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var command = new LoginUserAccountCommand(login_dto.Email, login_dto.Password);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(new { Message = "Login realizado com sucesso.", TokenKey = JwtTokenHandler.GenerateToken(result.Data) });
        }

        catch
        {
            return StatusCode(500, new { Message = "Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}