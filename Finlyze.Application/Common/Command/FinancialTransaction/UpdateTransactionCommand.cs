namespace Finlyze.Application.Abstracts.Interfaces.Commands;

public class UpdateFinancialTransactionCommand
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public int TranType { get; set; }
    public DateTime CreateAt { get; set; }

    public UpdateFinancialTransactionCommand(int id, string title, string description, decimal amount, int type, DateTime create)
    {
        Id = id;
        Title = title;
        Description = description;
        Amount = amount;
        TranType = type;
        CreateAt = create;
    }
}