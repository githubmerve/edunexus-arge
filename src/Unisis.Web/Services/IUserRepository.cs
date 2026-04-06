using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IUserRepository
{
    Task<UserProfile?> GetUserAsync(string userId, CancellationToken cancellationToken = default);
    Task SaveUserProfileAsync(UserProfile profile, CancellationToken cancellationToken = default);
    Task SaveIssueReportAsync(IssueReport report, CancellationToken cancellationToken = default);
    Task SaveExcuseRequestAsync(ExcuseRequest request, CancellationToken cancellationToken = default);
    Task SaveAvailabilitySlotAsync(AvailabilitySlot slot, CancellationToken cancellationToken = default);
    Task SaveQrSessionAsync(QrLoginSession session, CancellationToken cancellationToken = default);
    Task<QrLoginSession?> GetQrSessionByHashAsync(string tokenHash, CancellationToken cancellationToken = default);
}
