namespace Finlyze.Application.Abstract.Interface.Command;

public class CreateFinancialTransactionCommand
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public int TranType { get; set; }
    public DateTime CreateAt { get; set; }
    public Guid UserAccountId { get; set; }

    public CreateFinancialTransactionCommand(string title, string? description, decimal amount, int type, DateTime create, Guid userAccountId)
    {
        Title = title;
        Description = description;
        Amount = amount;
        TranType = type;
        CreateAt = create;
        UserAccountId = userAccountId;
    }
}