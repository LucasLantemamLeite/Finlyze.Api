using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.TransactionObjects;

public class TypeTransaction : ValueObject
{
    public EType Value { get; set; }

    public TypeTransaction(int type)
    {
        RoleException.ThrowIfNotDefined<EType>(type, "Type Inválido.");
        Value = (EType)type;
    }
}