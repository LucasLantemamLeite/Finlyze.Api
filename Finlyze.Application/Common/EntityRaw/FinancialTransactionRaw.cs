namespace Finlyze.Application.Entities.Raws;

public class FinancialTransactionRaw
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public int TranType { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public Guid UserAccountId { get; set; }
}