using System.Net.Sockets;
using Finlyze.Domain.ValueObject.TransactionObjects;

namespace Finlyze.Domain.Entity;

public class Transaction : Entity
{
    public TransactionTitle TransactionTitle { get; private set; }
    public TransactionDescription? Description { get; private set; }
    public Amount Amount { get; private set; }
    public TypeTransaction TypeTransaction { get; private set; }
    public TransactionCreateAt TransactionCreateAt { get; private set; }
    public TransactionUpdateAt TransactionUpdateAt { get; private set; }

    public Transaction(string title, string? description, decimal amount, int type, DateTime create)
    {
        TransactionTitle = new TransactionTitle(title);
        Description = new TransactionDescription(description);
        Amount = new Amount(amount);
        TypeTransaction = new TypeTransaction(type);
        TransactionCreateAt = new TransactionCreateAt(create);
        TransactionUpdateAt = new TransactionUpdateAt();
    }
}