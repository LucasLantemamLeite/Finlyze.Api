using System.ComponentModel.DataAnnotations;

namespace Finlyze.Application.Dtos;

public class DeleteFinancialTransactionDto
{
    [Required(ErrorMessage = "O campo de Id é obrigatório.")]
    public int Id { get; set; }
}