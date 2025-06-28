namespace Finlyze.Application.Dto;

public class UserAccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateTime CreateAt { get; set; }
    public bool Active { get; set; }
    public int Role { get; set; }
}