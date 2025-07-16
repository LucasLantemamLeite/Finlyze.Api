using System.ComponentModel.DataAnnotations;

namespace Finlyze.Application.Dto;

public class LoginUserAccountDto
{
    [Required(ErrorMessage = "O campo de Email é obrigatório.")]
    [MaxLength(254, ErrorMessage = "O campo de Email deve ter no máximo 254 caracteres.")]
    [EmailAddress(ErrorMessage = "O campo Email não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo de Password é obrigatório.")]
    [MaxLength(30, ErrorMessage = "O campo Password deve ter no máximo 30 caracteres.")]
    public string Password { get; set; }
}