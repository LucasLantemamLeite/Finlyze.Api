using Finlyze.Domain.Entities.FinancialTransactionEntity;

namespace Finlyze.Application.Entities.Raws.Converts;

public static class MapRawToFinancialTransaction
{
    public static FinancialTransaction ToSingleTransaction(this FinancialTransactionRaw transaction_raw) => new FinancialTransaction(transaction_raw.Id, transaction_raw.Title, transaction_raw.Description, transaction_raw.Amount, transaction_raw.TranType, transaction_raw.CreateAt, transaction_raw.UpdateAt, transaction_raw.UserAccountId);
    public static IEnumerable<FinancialTransaction> ToEnumerableTransaction(this IEnumerable<FinancialTransactionRaw> raws) => raws.Select(raw => raw.ToSingleTransaction());
}