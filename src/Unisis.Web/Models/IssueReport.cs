using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class IssueReport
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    [FirestoreProperty]
    public string StudentId { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Category { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Location { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Description { get; set; } = string.Empty;
    [FirestoreProperty]
    public List<string> AttachmentUrls { get; set; } = [];
    [FirestoreProperty]
    public string Status { get; set; } = "Open";
    [FirestoreProperty]
    public string AssignedUnit { get; set; } = "Support";
    [FirestoreProperty]
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    [FirestoreProperty]
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
