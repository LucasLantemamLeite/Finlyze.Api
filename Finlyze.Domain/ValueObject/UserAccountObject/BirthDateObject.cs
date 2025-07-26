using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class BirthDate : ValueObject
{
    public DateOnly Value { get; set; }

    public BirthDate(DateOnly birth)
    {
        BirthDateException.ThrowIfFalse(birth, "Data não pode ser futura.", "Usuário não pode ser menor de idade.");
        Value = birth;
    }

    private BirthDate() { }
}