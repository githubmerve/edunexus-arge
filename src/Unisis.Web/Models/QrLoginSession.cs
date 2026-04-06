using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class QrLoginSession
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    [FirestoreProperty]
    public string StudentId { get; set; } = string.Empty;
    [FirestoreProperty]
    public string TokenHash { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Nonce { get; set; } = Guid.NewGuid().ToString("N");
    [FirestoreProperty]
    public DateTime ExpiresAtUtc { get; set; }
    [FirestoreProperty]
    public DateTime? UsedAtUtc { get; set; }
    [FirestoreProperty]
    public string DeviceId { get; set; } = string.Empty;
}
