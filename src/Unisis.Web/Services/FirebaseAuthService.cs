using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class FirebaseAuthService : IFirebaseAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FirebaseClientOptions _options;

    public FirebaseAuthService(
        IHttpClientFactory httpClientFactory,
        IOptions<FirebaseClientOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<FirebaseSignInResponse?> SignInAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (!_options.Enabled || string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            return null;
        }

        var client = _httpClientFactory.CreateClient();
        var payload = new
        {
            email,
            password,
            returnSecureToken = true
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_options.ApiKey}")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json")
        };

        try
        {
            using var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<FirebaseSignInResponse>(body);
        }
        catch
        {
            return null;
        }
    }
}
