using Finlyze.Domain.ValueObject.TransactionObjects;

namespace Finlyze.Domain.Entity;

public class Transaction : Entity
{
    public TransactionTitle TransactionTitle { get; private set; }
    public TransactionDescription? TransactionDescription { get; private set; }
    public Amount Amount { get; private set; }
    public TypeTransaction TypeTransaction { get; private set; }
    public TransactionCreateAt TransactionCreateAt { get; private set; }
    public TransactionUpdateAt TransactionUpdateAt { get; private set; }
    public Guid UserAccountId { get; private set; }
    public UserAccount UserAccount { get; set; }

    public Transaction(string title, string? description, decimal amount, int type, DateTime create)
    {
        TransactionTitle = new TransactionTitle(title);
        TransactionDescription = new TransactionDescription(description);
        Amount = new Amount(amount);
        TypeTransaction = new TypeTransaction(type);
        TransactionCreateAt = new TransactionCreateAt(create);
        TransactionUpdateAt = new TransactionUpdateAt();
    }

    private Transaction() { }
}