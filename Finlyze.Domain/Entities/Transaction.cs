using Finlyze.Domain.ValueObject.TransactionObjects;

namespace Finlyze.Domain.Entity;

public class Transaction : EntityInt
{
    public TransactionTitle TransactionTitle { get; private set; }
    public TransactionDescription? TransactionDescription { get; private set; }
    public Amount Amount { get; private set; }
    public TypeTransaction TypeTransaction { get; private set; }
    public TransactionCreateAt TransactionCreateAt { get; private set; }
    public TransactionUpdateAt TransactionUpdateAt { get; private set; }
    public Guid UserAccountId { get; private set; }
    public UserAccount UserAccount { get; set; }

    public void ChangeId(int id) => Id = id;

    public Transaction(string title, string? description, decimal amount, int type, DateTime create, Guid userAccountId)
    {
        TransactionTitle = new TransactionTitle(title);
        TransactionDescription = new TransactionDescription(description);
        Amount = new Amount(amount);
        TypeTransaction = new TypeTransaction(type);
        TransactionCreateAt = new TransactionCreateAt(create);
        TransactionUpdateAt = new TransactionUpdateAt();
        UserAccountId = userAccountId;
    }

    public Transaction(int id, string title, string? description, decimal amount, int type, DateTime create, DateTime update, Guid userAccountId) : base(id)
    {
        TransactionTitle = new TransactionTitle(title);
        TransactionDescription = new TransactionDescription(description);
        Amount = new Amount(amount);
        TypeTransaction = new TypeTransaction(type);
        TransactionCreateAt = new TransactionCreateAt(create);
        TransactionUpdateAt = new TransactionUpdateAt(update);
        UserAccountId = userAccountId;
    }

    private Transaction() { }
}