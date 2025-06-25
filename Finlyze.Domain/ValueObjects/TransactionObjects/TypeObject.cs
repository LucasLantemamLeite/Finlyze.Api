using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class Type : ValueObject
{
    public EType Value { get; set; }

    public Type(int type)
    {
        Value = (EType)type;
    }
}