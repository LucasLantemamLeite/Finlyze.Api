using Finlyze.Domain.ValueObjects.Base;
using Finlyze.Domain.ValueObjects.Enums;
using Finlyze.Domain.ValueObjects.Validations.Exceptions;

namespace Finlyze.Domain.ValueObjects.SystemLogObjects;

public class LogType : ValueObject
{
    public ELog Value { get; private set; }

    public LogType(int log)
    {
        EnumException.ThrowIfNotDefined<ELog>(log, "Enum Inválido.");
        Value = (ELog)log;
    }

    private LogType() { }
}