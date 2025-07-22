namespace Finlyze.Application.Abstract.Interface.Command;

public class DeleteFinancialTransactionCommand

{
    public int Id { get; set; }

    public DeleteFinancialTransactionCommand(int id) => Id = id;
}