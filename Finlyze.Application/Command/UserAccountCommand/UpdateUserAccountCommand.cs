namespace Finlyze.Application.Abstract.Interface.Command;

public class UpdateUserAccountCommand
{
    public Guid Id { get; }
    public string ConfirmPassword { get; set; }
    public string? Name { get; }
    public string? Email { get; }
    public string? Password { get; }
    public string? PhoneNumber { get; }
    public DateOnly? BirthDate { get; set; }

    public UpdateUserAccountCommand(Guid id, string confirm_password, string name, string email, string password, string phone_number, DateOnly birth)
    {
        Id = id;
        ConfirmPassword = confirm_password;
        Name = name;
        Email = email;
        Password = password;
        PhoneNumber = phone_number;
        BirthDate = birth;
    }
}