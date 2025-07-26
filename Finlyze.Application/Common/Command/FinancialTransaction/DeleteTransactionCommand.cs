namespace Finlyze.Application.Abstracts.Interfaces.Commands;

public class DeleteFinancialTransactionCommand
{
    public int Id { get; set; }

    public DeleteFinancialTransactionCommand(int id) => Id = id;
}