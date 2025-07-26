using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Dtos;
using Finlyze.Application.Abstracts.Interfaces.Commands;

namespace Finlyze.Api.Controllers.FinancialTransactionControllers;

[ApiController]
[Route("api/v1")]
[Tags("Transactions")]
public class CreateFinancialTransactionController : ControllerBase
{
    private readonly ICreateFinancialTransactionHandler _handler;

    public CreateFinancialTransactionController(ICreateFinancialTransactionHandler handler) => _handler = handler;

    [Authorize]
    [HttpPost("transactions")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFinancialTransactionDto tran_dto)
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

            var command = new CreateFinancialTransactionCommand(tran_dto.Title, tran_dto.Description, tran_dto.Amount, tran_dto.Type, tran_dto.CreateAt, guidId);
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