using System.Security.Claims;
using Finlyze.Application.Abstracts.Interfaces.Commands;
using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Controllers.FinancialTransactionControllers;

[ApiController]
[Route("api/v1")]
[Tags("Transactions")]
public class DeleteFinancialTrasactionController : ControllerBase
{
    private readonly IDeleteFinancialTransactionHandler _handler;

    public DeleteFinancialTrasactionController(IDeleteFinancialTransactionHandler handler) => _handler = handler;

    [Authorize]
    [HttpDelete("transactions")]
    public async Task<IActionResult> CreateAsync([FromBody] DeleteFinancialTransactionDto tran_dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();

            var command = new DeleteFinancialTransactionCommand(tran_dto.Id);
            var result = await _handler.Handle(command);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { Message = "Transaction deletada com sucesso." });
        }

        catch
        {
            return StatusCode(500, new { Message = $"Erro interno do servidor. Tente novamente mais tarde." });
        }
    }
}