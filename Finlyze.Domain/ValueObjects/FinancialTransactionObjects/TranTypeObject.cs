using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TranType : ValueObject
{
    public EType Value { get; private set; }

    public TranType(int type)
    {
        EnumException.ThrowIfNotDefined<EType>(type, "Type Inválido.");
        Value = (EType)type;
    }

    private TranType() { }
}