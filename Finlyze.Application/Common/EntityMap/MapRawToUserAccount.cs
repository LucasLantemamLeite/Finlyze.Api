using Finlyze.Domain.Entity;

namespace Finlyze.Application.Entity.Raw.Convert;

public static class MapRawToUserAccount
{
    public static UserAccount ToUserAccount(this UserAccountRaw user_raw) => new UserAccount(user_raw.Id, user_raw.Name, user_raw.Email, user_raw.Password, user_raw.PhoneNumber, DateOnly.FromDateTime(user_raw.BirthDate), user_raw.CreateAt, user_raw.Active, user_raw.Role);
}