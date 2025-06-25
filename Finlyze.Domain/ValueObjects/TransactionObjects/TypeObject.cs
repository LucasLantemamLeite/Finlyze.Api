using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TypeTransaction : ValueObject
{
    public EType Value { get; set; }

    public TypeTransaction(int type)
    {
        Value = (EType)type;
    }
}