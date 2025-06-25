using Finlyze.Domain.ValueObject.Enums;

namespace Finlyze.Domain.ValueObject.AppLogObjects;

public class LogType : ValueObject
{
    public ELog Value { get; set; }

    public LogType(int log)
    {
        Value = (ELog)log;
    }
}