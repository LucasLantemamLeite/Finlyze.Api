namespace Finlyze.Application.Dto;

public class AppLogDto
{
    public Guid Id { get; set; }
    public string LogTitle { get; set; }
    public string LogDescription { get; set; }
    public DateTime LogCreateAt { get; set; }
}