using Google.Cloud.Firestore;

namespace Unisis.Web.Services;

public interface IFirebaseInitializer
{
    void Initialize();
    FirestoreDb GetDb();
    bool IsConfigured { get; }
}
