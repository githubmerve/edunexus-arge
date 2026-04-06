using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class LocalDemoRepository : IUserRepository
{
    private static readonly Dictionary<string, UserProfile> Users = new();
    private static readonly Dictionary<string, IssueReport> IssueReports = new();
    private static readonly Dictionary<string, ExcuseRequest> ExcuseRequests = new();
    private static readonly Dictionary<string, AvailabilitySlot> AvailabilitySlots = new();
    private static readonly Dictionary<string, QrLoginSession> QrSessions = new();

    public Task<UserProfile?> GetUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        Users.TryGetValue(userId, out var user);
        return Task.FromResult(user);
    }

    public Task SaveUserProfileAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        Users[profile.UniversityId] = profile;
        return Task.CompletedTask;
    }

    public Task SaveIssueReportAsync(IssueReport report, CancellationToken cancellationToken = default)
    {
        IssueReports[report.Id] = report;
        return Task.CompletedTask;
    }

    public Task SaveExcuseRequestAsync(ExcuseRequest request, CancellationToken cancellationToken = default)
    {
        ExcuseRequests[request.Id] = request;
        return Task.CompletedTask;
    }

    public Task SaveAvailabilitySlotAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default)
    {
        AvailabilitySlots[slot.Id] = slot;
        return Task.CompletedTask;
    }

    public Task SaveQrSessionAsync(QrLoginSession session, CancellationToken cancellationToken = default)
    {
        QrSessions[session.Id] = session;
        return Task.CompletedTask;
    }

    public Task<QrLoginSession?> GetQrSessionByHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        var result = QrSessions.Values.FirstOrDefault(x => x.TokenHash == tokenHash);
        return Task.FromResult(result);
    }
}
