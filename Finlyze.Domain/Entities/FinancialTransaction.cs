using Finlyze.Domain.ValueObject.TransactionObjects;

namespace Finlyze.Domain.Entity;

public class FinancialTransaction : EntityInt
{
    public Title Title { get; private set; }
    public Description? Description { get; private set; }
    public Amount Amount { get; private set; }
    public TranType TranType { get; private set; }
    public CreateAt CreateAt { get; private set; }
    public UpdateAt UpdateAt { get; private set; }
    public Guid UserAccountId { get; private set; }
    public UserAccount UserAccount { get; set; }

    public void ChangeId(int id) => Id = id;

    public void ChangeTitle(string title)
    {
        if (title is not null && title != Title.Value)
            Title = new Title(title);
    }

    public void ChangeDescription(string? description)
    {
        if (description != Description?.Value)
            Description = new Description(description);
    }

    public void ChangeAmount(decimal amount)
    {
        if (amount != Amount.Value)
            Amount = new Amount(amount);
    }

    public void ChangeType(int type)
    {
        if (type != (int)TranType.Value)
            TranType = new TranType(type);
    }

    public void ChangeCreate(DateTime create)
    {
        if (create != CreateAt.Value)
            CreateAt = new CreateAt(create);
    }

    public void ChangeUpdate() => UpdateAt = new UpdateAt();

    public FinancialTransaction(string title, string? description, decimal amount, int type, DateTime create, Guid userAccountId)
    {
        Title = new Title(title);
        Description = new Description(description);
        Amount = new Amount(amount);
        TranType = new TranType(type);
        CreateAt = new CreateAt(create);
        UpdateAt = new UpdateAt();
        UserAccountId = userAccountId;
    }

    public FinancialTransaction(int id, string title, string? description, decimal amount, int type, DateTime create, DateTime update, Guid userAccountId) : base(id)
    {
        Title = new Title(title);
        Description = new Description(description);
        Amount = new Amount(amount);
        TranType = new TranType(type);
        CreateAt = new CreateAt(create);
        UpdateAt = new UpdateAt(update);
        UserAccountId = userAccountId;
    }

    private FinancialTransaction() { }
}