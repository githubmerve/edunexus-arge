using System.Text.Json;
using Unisis.Web.Models;

namespace Unisis.Web.Extensions;

public static class SessionExtensions
{
    private const string CurrentUserKey = "CurrentUser";

    public static void SetCurrentUser(this ISession session, AppUser user)
    {
        session.SetString(CurrentUserKey, JsonSerializer.Serialize(user));
    }

    public static AppUser? GetCurrentUser(this ISession session)
    {
        var value = session.GetString(CurrentUserKey);
        return string.IsNullOrWhiteSpace(value)
            ? null
            : JsonSerializer.Deserialize<AppUser>(value);
    }

    public static bool IsAuthenticated(this ISession session)
    {
        return GetCurrentUser(session) is not null;
    }
}
