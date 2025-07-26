using System.Security.Claims;
using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controllers.UserAccountControllers;

[ApiController]
[Route("api/v1")]
[Tags("Users")]
public class UpdateUserAccountController : ControllerBase
{
    private readonly IUpdateUserAccountHandler _handler;

    public UpdateUserAccountController(IUpdateUserAccountHandler handler) => _handler = handler;

    [Authorize]
    [HttpPatch("users")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserAccountDto user_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            Guid guidId;

            if (!Guid.TryParse(userId, out guidId))
                return BadRequest(new { Message = "Id inválido." });

            var command = new UpdateUserAccountCommand(guidId, user_dto.ConfirmPassword, user_dto.Name, user_dto.Email, user_dto.Password, user_dto.PhoneNumber, user_dto.BirthDate);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(new { Message = $"Dados alterados com sucesso da conta: {guidId}" });
        }

        catch
        {
            return StatusCode(500, new { Message = "Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}