using Finlyze.Domain.ValueObject.AppLogObjects;

namespace Finlyze.Domain.Entity;

public class AppLog : Entity
{
    public LogType LogType { get; private set; }
    public LogTitle LogTitle { get; private set; }
    public LogDescription LogDescription { get; private set; }
    public LogCreateAt LogCreateAt { get; private set; }

    public AppLog(int log, string title, string description)
    {
        LogType = new LogType(log);
        LogTitle = new LogTitle(title);
        LogDescription = new LogDescription(description);
        LogCreateAt = new LogCreateAt();
    }
}