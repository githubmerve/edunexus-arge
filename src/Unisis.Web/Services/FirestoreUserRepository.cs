using Google.Cloud.Firestore;
using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class FirestoreUserRepository : IUserRepository
{
    private readonly FirestoreDb _db;

    public FirestoreUserRepository(IFirebaseInitializer initializer)
    {
        _db = initializer.GetDb();
    }

    public async Task<UserProfile?> GetUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection("users").Document(userId).GetSnapshotAsync(cancellationToken);
        return snapshot.Exists ? snapshot.ConvertTo<UserProfile>() : null;
    }

    public Task SaveUserProfileAsync(UserProfile profile, CancellationToken cancellationToken = default)
    {
        return _db.Collection("users")
            .Document(profile.UniversityId)
            .SetAsync(profile, cancellationToken: cancellationToken);
    }

    public Task SaveIssueReportAsync(IssueReport report, CancellationToken cancellationToken = default)
    {
        return _db.Collection("issueReports")
            .Document(report.Id)
            .SetAsync(report, cancellationToken: cancellationToken);
    }

    public Task SaveExcuseRequestAsync(ExcuseRequest request, CancellationToken cancellationToken = default)
    {
        return _db.Collection("excuseRequests")
            .Document(request.Id)
            .SetAsync(request, cancellationToken: cancellationToken);
    }

    public Task SaveAvailabilitySlotAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default)
    {
        return _db.Collection("availabilitySlots")
            .Document(slot.Id)
            .SetAsync(slot, cancellationToken: cancellationToken);
    }

    public Task SaveQrSessionAsync(QrLoginSession session, CancellationToken cancellationToken = default)
    {
        return _db.Collection("qrLoginSessions")
            .Document(session.Id)
            .SetAsync(session, cancellationToken: cancellationToken);
    }

    public async Task<QrLoginSession?> GetQrSessionByHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        var snapshot = await _db.Collection("qrLoginSessions")
            .WhereEqualTo(nameof(QrLoginSession.TokenHash), tokenHash)
            .Limit(1)
            .GetSnapshotAsync(cancellationToken);

        return snapshot.Documents.FirstOrDefault()?.ConvertTo<QrLoginSession>();
    }
}
