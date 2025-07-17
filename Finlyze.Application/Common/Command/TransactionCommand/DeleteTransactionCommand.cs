namespace Finlyze.Application.Abstract.Interface.Command;

public class DeleteTransactionCommand

{
    public int Id { get; set; }

    public DeleteTransactionCommand(int id) => Id = id;
}