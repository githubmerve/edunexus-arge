using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class AcademicianProfile : UserProfile
{
    [FirestoreProperty]
    public string Faculty { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Department { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;
    [FirestoreProperty]
    public string OfficeLocation { get; set; } = string.Empty;
}
