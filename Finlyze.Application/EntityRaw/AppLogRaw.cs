namespace Finlyze.Application.Entity.Raw;

public class AppLogRaw
{
    public int Id { get; set; }
    public int LogType { get; set; }
    public string LogTitle { get; set; }
    public string LogDescription { get; set; }
    public DateTime LogCreateAt { get; set; }
}