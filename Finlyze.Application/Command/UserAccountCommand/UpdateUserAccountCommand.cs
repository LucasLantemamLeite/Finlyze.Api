namespace Finlyze.Application.Abstract.Interface.Command;

public class UpdateUserAccountCommand
{
    public Guid Id { get; }
    public string? Name { get; }
    public string? Email { get; }
    public string? Password { get; }
    public string? PhoneNumber { get; }
}