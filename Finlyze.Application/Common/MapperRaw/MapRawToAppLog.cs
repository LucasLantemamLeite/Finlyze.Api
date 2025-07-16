using Finlyze.Domain.Entity;

namespace Finlyze.Application.Entity.Raw.Convert;

public static class MapRawToAppLog
{
    public static SystemLog ToSingleAppLog(this AppLogRaw app_raw) => new SystemLog(app_raw.Id, app_raw.LogType, app_raw.Title, app_raw.Description, app_raw.CreateAt);
    public static IEnumerable<SystemLog> ToEnumerableAppLog(this IEnumerable<AppLogRaw> logs) => logs.Select(log => log.ToSingleAppLog());
}