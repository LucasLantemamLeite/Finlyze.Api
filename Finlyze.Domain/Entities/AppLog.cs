using Finlyze.Domain.ValueObject.AppLogObjects;

namespace Finlyze.Domain.Entity;

public class AppLog : Entity
{
    public LogType LogType { get; set; }
    public LogTitle LogTitle { get; set; }
    public LogDescription LogDescription { get; set; }
    public LogCreateAt CreateAt { get; set; }

    public AppLog(int log, string title, string description)
    {
        LogType = new LogType(log);
        LogTitle = new LogTitle(title);
        LogDescription = new LogDescription(description);
        CreateAt = new LogCreateAt();
    }
}