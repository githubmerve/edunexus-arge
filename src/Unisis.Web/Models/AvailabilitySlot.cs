using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class AvailabilitySlot
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    [FirestoreProperty]
    public string AcademicianId { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Date { get; set; } = string.Empty;
    [FirestoreProperty]
    public string StartTime { get; set; } = string.Empty;
    [FirestoreProperty]
    public string EndTime { get; set; } = string.Empty;
    [FirestoreProperty]
    public bool IsAvailable { get; set; } = true;
    [FirestoreProperty]
    public string Note { get; set; } = string.Empty;
}
