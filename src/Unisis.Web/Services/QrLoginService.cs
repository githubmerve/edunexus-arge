using System.Security.Cryptography;
using System.Text;
using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class QrLoginService : IQrLoginService
{
    private readonly IUserRepository _repository;

    public QrLoginService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> CreateLoginTokenAsync(string studentId, string deviceId, CancellationToken cancellationToken = default)
    {
        var rawToken = $"{studentId}:{deviceId}:{Guid.NewGuid():N}:{DateTime.UtcNow:O}";
        var session = new QrLoginSession
        {
            StudentId = studentId,
            DeviceId = deviceId,
            ExpiresAtUtc = DateTime.UtcNow.AddSeconds(45),
            TokenHash = ComputeSha256(rawToken)
        };

        await _repository.SaveQrSessionAsync(session, cancellationToken);
        return rawToken;
    }

    public async Task<bool> ValidateLoginTokenAsync(string rawToken, CancellationToken cancellationToken = default)
    {
        var tokenHash = ComputeSha256(rawToken);
        var session = await _repository.GetQrSessionByHashAsync(tokenHash, cancellationToken);

        if (session is null)
        {
            return false;
        }

        if (session.UsedAtUtc.HasValue || session.ExpiresAtUtc <= DateTime.UtcNow)
        {
            return false;
        }

        session.UsedAtUtc = DateTime.UtcNow;
        await _repository.SaveQrSessionAsync(session, cancellationToken);
        return true;
    }

    private static string ComputeSha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }
}
