using System.Net.Sockets;
using Finlyze.Domain.ValueObject.TransactionObjects;

namespace Finlyze.Domain.Entity;

public class Transaction : Entity
{
    public Title Title { get; private set; }
    public Description? Description { get; private set; }
    public Amount Amount { get; private set; }
    public TypeTransaction TypeTransaction { get; private set; }
    public CreateAt CreateAt { get; private set; }
    public UpdateAt UpdateAt { get; private set; }

    public Transaction(string title, string? description, decimal amount, int type, DateTime create)
    {
        Title = new Title(title);
        Description = new Description(description);
        Amount = new Amount(amount);
        TypeTransaction = new TypeTransaction(type);
        CreateAt = new CreateAt(create);
        UpdateAt = new UpdateAt();
    }
}