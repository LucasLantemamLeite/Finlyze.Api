using Finlyze.Domain.ValueObjects.Base;

namespace Finlyze.Domain.ValueObjects.UserAccountObjects;

public class Active : ValueObject
{
    public bool Value { get; set; }

    public Active(bool active)
    {
        Value = active;
    }

    private Active() { }
}