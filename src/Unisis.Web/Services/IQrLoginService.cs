namespace Unisis.Web.Services;

public interface IQrLoginService
{
    Task<string> CreateLoginTokenAsync(string studentId, string deviceId, CancellationToken cancellationToken = default);
    Task<bool> ValidateLoginTokenAsync(string rawToken, CancellationToken cancellationToken = default);
}
