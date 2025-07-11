using System.Security.Claims;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controller.Users;

[ApiController]
[Route("api/v1")]
[Tags("Users")]
public class DeleteUserAccountController : ControllerBase
{
    private readonly IDeleteAccountHandler _handler;
    public DeleteUserAccountController(IDeleteAccountHandler handler) => _handler = handler;

    [Authorize]
    [HttpDelete("users")]
    public async Task<IActionResult> DeleteAsync()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            Guid guidId;

            if (!Guid.TryParse(userId, out guidId))
                return BadRequest(new { Message = "Id inválido." });

            var command = new DeleteUserAccountCommand(guidId);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(new { Message = "Conta deletada com sucesso." });
        }

        catch
        {
            return StatusCode(500, new { Message = "Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}