namespace Finlyze.Application.Abstract.Interface.Command;

public class UpdateTransactionCommand
{
    public string TransactionTitle { get; set; }
    public string? TransactionDescription { get; set; }
    public decimal Amount { get; set; }
    public int TypeTransaction { get; set; }
    public DateTime TransactionCreateAt { get; set; }
    public DateTime TransactionUpdateAt { get; set; }

    public UpdateTransactionCommand(string title, string description, decimal amount, int type, DateTime create, DateTime update)
    {
        TransactionTitle = title;
        TransactionDescription = description;
        Amount = amount;
        TypeTransaction = type;
        TransactionCreateAt = create;
        TransactionUpdateAt = update;
    }
}