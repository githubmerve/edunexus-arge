namespace Unisis.Web.Services;

public class FirebaseClientOptions
{
    public const string SectionName = "FirebaseClient";

    public bool Enabled { get; set; } = true;
    public string ApiKey { get; set; } = string.Empty;
    public string AuthDomain { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string StorageBucket { get; set; } = string.Empty;
    public string MessagingSenderId { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
    public string MeasurementId { get; set; } = string.Empty;
}
