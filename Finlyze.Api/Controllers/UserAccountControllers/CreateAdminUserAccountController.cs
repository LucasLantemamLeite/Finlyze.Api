using System.Security.Claims;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controller.Users;

[ApiController]
[Route("api/v1")]
[Tags("Users")]
public class CreateAdminUserAccountController : ControllerBase
{
    private readonly ICreateAdminUserAccountHandler _handler;
    public CreateAdminUserAccountController(ICreateAdminUserAccountHandler handler) => _handler = handler;

    [Authorize]
    [HttpPost("users-admin")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserAccountDto user_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var command = new CreateUserAccountCommand(user_dto.Name, user_dto.Email, user_dto.Password, user_dto.PhoneNumber, user_dto.BirthDate);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(new { Message = "Conta de Administrador criado com sucesso." });
        }

        catch
        {
            return StatusCode(500, new { Message = "Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}