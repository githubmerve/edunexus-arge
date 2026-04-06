using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class UserProfile
{
    [FirestoreProperty]
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    [FirestoreProperty]
    public string FirebaseUid { get; set; } = string.Empty;
    [FirestoreProperty]
    public string UniversityId { get; set; } = string.Empty;
    [FirestoreProperty]
    public string FullName { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Email { get; set; } = string.Empty;
    [FirestoreProperty]
    public UserRole Role { get; set; }
    [FirestoreProperty]
    public bool IsActive { get; set; } = true;
}
