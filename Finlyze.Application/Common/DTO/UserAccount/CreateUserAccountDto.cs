using System.ComponentModel.DataAnnotations;

namespace Finlyze.Application.Dtos;

public class CreateUserAccountDto
{
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [MaxLength(254, ErrorMessage = "O campo Email deve ter no máximo 254 caracteres.")]
    [EmailAddress(ErrorMessage = "O campo Email não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Password é obrigatório.")]
    [MaxLength(30, ErrorMessage = "O campo Password deve ter no máximo 30 caracteres.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo PhoneNumber é obrigatório.")]
    [MaxLength(15, ErrorMessage = "O campo PhoneNumber deve ter no máximo 15 caracteres.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "O campo BirthDate é obrigatório.")]
    [DataType(DataType.Date, ErrorMessage = "O campo BirthDate não é válido.")]
    public DateOnly BirthDate { get; set; }
}