using System.ComponentModel.DataAnnotations;

namespace Finlyze.Application.Dto;

public class DeleteFinancialTransactionDto
{
    [Required(ErrorMessage = "O campo de Id é obrigatório.")]
    public int Id { get; set; }
}