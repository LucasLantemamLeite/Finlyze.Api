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
                return BadRequest(ModelState);

            var command = new LoginUserAccountCommand(login_dto.Email, login_dto.Password);
            var result = await _handler.Handle(command);

            if (!result.Success && result.Message == "Credenciais incorretas.")
                return Unauthorized(result.Message);

            if (!result.Success)
                return BadRequest(result.Message);

            return StatusCode(200, new { result.Message, Token = JwtTokenHandler.GenerateToken(result.Data) });
        }

        catch (Exception e)
        {
            var errorMsg = e.InnerException?.Message ?? e.Message ?? "Erro desconhecido";
            return BadRequest($"Controller -> LoginUserAccountController -> LoginAsync: {errorMsg}");
        }
    }
}