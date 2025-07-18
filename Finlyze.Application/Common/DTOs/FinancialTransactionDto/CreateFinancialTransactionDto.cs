using System.ComponentModel.DataAnnotations;

namespace Finlyze.Application.Dto;

public class CreateFinancialTransactionDto
{
    [Required(ErrorMessage = "O campo de Title é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo de Title deve ter no máximo 100 caracteres.")]
    public string Title { get; set; }

    [MaxLength(300, ErrorMessage = "A Description deve ter no máximo 300 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo de Amount é obrigatório.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "O campo de Type é obrigatório.")]
    public int Type { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "O campo CreateAt é inválido.")]
    public DateTime CreateAt { get; set; }
}