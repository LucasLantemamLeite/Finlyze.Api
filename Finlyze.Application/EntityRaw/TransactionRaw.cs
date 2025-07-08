namespace Finlyze.Application.Entity.Raw;

public class TransactionRaw
{
    public Guid Id { get; set; }
    public string TransactionTitle { get; set; }
    public string? TransactionDescription { get; set; }
    public decimal Amount { get; set; }
    public int TypeTransaction { get; set; }
    public DateTime TransactionCreateAt { get; set; }
    public DateTime TransactionUpdateAt { get; set; }
    public Guid UserAccountId { get; set; }
}