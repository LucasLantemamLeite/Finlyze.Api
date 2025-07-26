namespace Finlyze.Application.Abstracts.Interfaces.Commands;

public class CreateUserAccountCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }

    public CreateUserAccountCommand(string name, string email, string password, string phone, DateOnly birth)
    {
        Name = name;
        Email = email;
        Password = password;
        PhoneNumber = phone;
        BirthDate = birth;
    }
}