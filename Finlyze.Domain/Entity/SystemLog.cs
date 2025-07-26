using Finlyze.Domain.Entities.Base;
using Finlyze.Domain.ValueObjects.SystemLogObjects;

namespace Finlyze.Domain.Entities.SystemLogEntity;

public sealed class SystemLog : EntityInt
{
    public LogType LogType { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public CreateAt CreateAt { get; private set; }

    public SystemLog(int log, string title, string description)
    {
        LogType = new LogType(log);
        Title = new Title(title);
        Description = new Description(description);
        CreateAt = new CreateAt();
    }

    public SystemLog(int id, int log, string title, string description, DateTime create) : base(id)
    {
        LogType = new LogType(log);
        Title = new Title(title);
        Description = new Description(description);
        CreateAt = new CreateAt(create);
    }

    private SystemLog() { }
}