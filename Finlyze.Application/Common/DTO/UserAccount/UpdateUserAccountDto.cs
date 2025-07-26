using System.ComponentModel.DataAnnotations;

namespace Finlyze.Application.Dtos;

public class UpdateUserAccountDto
{
    [Required(ErrorMessage = "O campo de Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo de Email é obrigatório.")]
    [MaxLength(254, ErrorMessage = "O campo Email deve ter no máximo 254 caracteres.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo de ConfirmPassword é obrigatório.")]
    [MaxLength(30, ErrorMessage = "O campo ConfirmPassword deve ter no máximo 30 caracteres.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "O campo de Password é obrigatório.")]
    [MaxLength(30, ErrorMessage = "O campo Password deve ter no máximo 30 caracteres.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo de PhoneNumber é obrigatório.")]
    [MaxLength(15, ErrorMessage = "O campo PhoneNumber deve ter no máximo 15 caracteres.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "O campo de BirthDate é obrigatório.")]
    [DataType(DataType.Date, ErrorMessage = "O campo BirthDate não é válido.")]
    public DateOnly BirthDate { get; set; }
}