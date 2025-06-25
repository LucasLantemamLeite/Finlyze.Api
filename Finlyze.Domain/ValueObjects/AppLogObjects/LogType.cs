using Finlyze.Domain.ValueObject.Enums;
using Finlyze.Domain.ValueObject.Validation;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class LogType : ValueObject
{
    public ELog Value { get; set; }

    public LogType(int log)
    {
        EnumException.ThrowIfNotDefined<ELog>(log, "Enum Inválido.");
        Value = (ELog)log;
    }
}