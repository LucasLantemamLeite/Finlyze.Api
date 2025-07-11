using Finlyze.Domain.Entity;

namespace Finlyze.Application.Entity.Raw.Convert;

public static class MapRawToTransaction
{
    public static Transaction ToSingleTransaction(this TransactionRaw transaction_raw) => new Transaction(transaction_raw.Id, transaction_raw.TransactionTitle, transaction_raw.TransactionDescription, transaction_raw.Amount, transaction_raw.TypeTransaction, transaction_raw.TransactionCreateAt, transaction_raw.TransactionUpdateAt);
    public static IEnumerable<Transaction> ToEnumerableTransaction(this IEnumerable<TransactionRaw> raws) => raws.Select(raw => raw.ToSingleTransaction());
}