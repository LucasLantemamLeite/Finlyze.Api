namespace Finlyze.Application.Abstract.Interface.Command;

public class DeleteUserAccountCommand
{
    public Guid Id { get; set; }

    public DeleteUserAccountCommand(Guid id)
    {
        Id = id;
    }
}