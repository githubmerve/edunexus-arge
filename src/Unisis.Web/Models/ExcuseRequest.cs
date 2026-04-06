using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class ExcuseRequest
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    [FirestoreProperty]
    public string StudentId { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Description { get; set; } = string.Empty;
    [FirestoreProperty]
    public string ExcuseType { get; set; } = string.Empty;
    [FirestoreProperty]
    public DateTime StartDateUtc { get; set; }
    [FirestoreProperty]
    public DateTime EndDateUtc { get; set; }
    [FirestoreProperty]
    public string? DocumentUrl { get; set; }
    [FirestoreProperty]
    public string Status { get; set; } = "Pending";
    [FirestoreProperty]
    public string? ReviewedByUserId { get; set; }
    [FirestoreProperty]
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    [FirestoreProperty]
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
