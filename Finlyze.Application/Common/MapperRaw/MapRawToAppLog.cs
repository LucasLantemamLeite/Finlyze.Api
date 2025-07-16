using Finlyze.Domain.Entity;

namespace Finlyze.Application.Entity.Raw.Convert;

public static class MapRawToAppLog
{
    public static AppLog ToSingleAppLog(this AppLogRaw app_raw) => new AppLog(app_raw.Id, app_raw.LogType, app_raw.LogTitle, app_raw.LogDescription, app_raw.LogCreateAt);
    public static IEnumerable<AppLog> ToEnumerableAppLog(this IEnumerable<AppLogRaw> logs) => logs.Select(log => log.ToSingleAppLog());
}