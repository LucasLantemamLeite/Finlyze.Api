namespace Finlyze.Application.Abstracts.Interfaces.Commands;

public class DeleteUserAccountCommand
{
    public Guid Id { get; set; }

    public DeleteUserAccountCommand(Guid id)
    {
        Id = id;
    }
}