namespace Finlyze.Application.Entities.Raws;

public class SystemLogRaw
{
    public int Id { get; set; }
    public int LogType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateAt { get; set; }
}