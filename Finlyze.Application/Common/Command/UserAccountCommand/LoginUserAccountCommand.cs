namespace Finlyze.Application.Abstract.Interface.Command;

public class LoginUserAccountCommand
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginUserAccountCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}