using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Enums;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.FinancialTransactionObjects;

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