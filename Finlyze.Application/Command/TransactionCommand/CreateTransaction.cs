namespace Finlyze.Application.Abstract.Interface.Command;

public class CreateTransactionCommand
{
    public string TransactionTitle { get; set; }
    public string? TransactionDescription { get; set; }
    public decimal Amount { get; set; }
    public int TypeTransaction { get; set; }
    public DateTime TransactionCreateAt { get; set; }
    public Guid UserAccount { get; set; }

    public CreateTransactionCommand(string title, string? description, decimal amount, int type, DateTime create, Guid userAccount)
    {
        TransactionTitle = title;
        TransactionDescription = description;
        Amount = amount;
        TypeTransaction = type;
        TransactionCreateAt = create;
        UserAccount = userAccount;
    }
}