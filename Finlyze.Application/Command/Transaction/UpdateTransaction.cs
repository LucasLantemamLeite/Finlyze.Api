namespace Finlyze.Application.Abstract.Interface.Command;

public class UpdateTransactionCommand
{
    public int Id { get; set; }
    public string TransactionTitle { get; set; }
    public string? TransactionDescription { get; set; }
    public decimal Amount { get; set; }
    public int TypeTransaction { get; set; }
    public DateTime TransactionCreateAt { get; set; }

    public UpdateTransactionCommand(int id, string title, string description, decimal amount, int type, DateTime create)
    {
        Id = id;
        TransactionTitle = title;
        TransactionDescription = description;
        Amount = amount;
        TypeTransaction = type;
        TransactionCreateAt = create;
    }
}