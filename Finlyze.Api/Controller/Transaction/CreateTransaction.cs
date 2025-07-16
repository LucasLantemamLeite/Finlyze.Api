using System.Security.Claims;
using Finlyze.Application.Abstract.Interface.Command;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controller.Transactions;

[ApiController]
[Route("api/v1")]
[Tags("Transactions")]
public class CreateUserAccountController : ControllerBase
{
    private readonly ICreateTransactionHandler _handler;

    public CreateUserAccountController(ICreateTransactionHandler handler) => _handler = handler;

    [Authorize]
    [HttpPost("transaction")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTransactionDto tran_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();

            Guid guidId;

            if (!Guid.TryParse(userId, out guidId))
                return BadRequest(new { Message = "Id inválido." });

            var command = new CreateTransactionCommand(tran_dto.Title, tran_dto.Description, tran_dto.Amount, tran_dto.Type, tran_dto.CreateAt, guidId);
            var result = await _handler.Handle(command);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return Ok(new { Message = "Transaction criado com sucesso." });
        }

        catch
        {
            return StatusCode(500, new { Message = $"Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}