using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;

namespace Unisis.Web.Services;

public class FirebaseInitializer : IFirebaseInitializer
{
    private readonly FirebaseOptions _options;
    private FirestoreDb? _db;
    private bool _isConfigured;

    public FirebaseInitializer(IOptions<FirebaseOptions> options)
    {
        _options = options.Value;
    }

    public bool IsConfigured => _isConfigured;

    public void Initialize()
    {
        if (string.IsNullOrWhiteSpace(_options.ServiceAccountPath) ||
            !File.Exists(_options.ServiceAccountPath))
        {
            _isConfigured = false;
            return;
        }

        if (FirebaseApp.DefaultInstance is null)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(_options.ServiceAccountPath),
                ProjectId = _options.ProjectId
            });
        }

        _db ??= new FirestoreDbBuilder
        {
            ProjectId = _options.ProjectId,
            Credential = GoogleCredential.FromFile(_options.ServiceAccountPath)
        }.Build();
        _isConfigured = true;
    }

    public FirestoreDb GetDb()
    {
        if (!_isConfigured)
        {
            throw new InvalidOperationException(
                "Firebase henuz yapilandirilmamis. Service account dosyasini ekleyin veya appsettings.json icindeki ServiceAccountPath degerini guncelleyin.");
        }

        _db ??= new FirestoreDbBuilder
        {
            ProjectId = _options.ProjectId,
            Credential = GoogleCredential.FromFile(_options.ServiceAccountPath)
        }.Build();

        return _db;
    }
}
