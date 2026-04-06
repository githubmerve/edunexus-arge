using Google.Cloud.Firestore;

namespace Unisis.Web.Models;

[FirestoreData]
public class StudentProfile : UserProfile
{
    [FirestoreProperty]
    public string Faculty { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Department { get; set; } = string.Empty;
    [FirestoreProperty]
    public string ClassYear { get; set; } = string.Empty;
    [FirestoreProperty]
    public string AdvisorAcademicianId { get; set; } = string.Empty;
}
