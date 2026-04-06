using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class GeminiService : IGeminiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly GeminiOptions _options;

    private static readonly string[] FallbackModels =
    [
        "gemini-2.0-flash",
        "gemini-2.5-flash",
        "gemini-1.5-flash"
    ];

    public GeminiService(IHttpClientFactory httpClientFactory, IOptions<GeminiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<string> GenerateContentAsync(string prompt, IEnumerable<AiChatTurn>? history = null)
    {
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            return string.Empty;
        }

        var contents = new List<object>();

        contents.Add(new
        {
            role = "user",
            parts = new[]
            {
                new
                {
                    text = """
                           Sen Bursa Uludag Universitesi icin calisan bir dijital kampus asistanisin (Edunexus).
                           Yanitlarini dogal ve duzgun Turkce ile ver.
                           Yonetim Bilisim Sistemleri, Isletme, Uluslararasi Ticaret, ogrenci surecleri,
                           mazeret basvurulari, teknik ariza bildirimleri ve genel universite yonlendirmeleri hakkinda yardimci ol.
                           Ogrencilerin akademik takvimi ve duyurulari takip etmelerine rehberlik et.
                           """
                }
            }
        });

        contents.Add(new
        {
            role = "model",
            parts = new[]
            {
                new
                {
                    text = "Anlasildi. Edunexus dijital kampus asistani olarak Bursa Uludag Universitesi ogrencilerine ve personeline yardimci olmaya hazirim."
                }
            }
        });

        if (history != null)
        {
            foreach (var turn in history)
            {
                contents.Add(new
                {
                    role = turn.Role == "assistant" ? "model" : "user",
                    parts = new[] { new { text = turn.Text } }
                });
            }
        }

        contents.Add(new
        {
            role = "user",
            parts = new[] { new { text = prompt } }
        });

        var payload = new
        {
            contents = contents.ToArray()
        };

        var candidateModels = new[] { _options.ModelId }
            .Concat(FallbackModels)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var client = _httpClientFactory.CreateClient();
        string? lastError = null;

        foreach (var model in candidateModels)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_options.ApiKey}";

            using var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json")
            };

            try
            {
                using var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    lastError = $"{response.StatusCode}: {errorBody}";

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                        response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        continue;
                    }

                    return $"Gemini servisi su an yanit veremiyor. ({response.StatusCode})";
                }

                var content = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(content);

                if (document.RootElement.TryGetProperty("candidates", out var candidates) &&
                    candidates.GetArrayLength() > 0 &&
                    candidates[0].TryGetProperty("content", out var resultContent) &&
                    resultContent.TryGetProperty("parts", out var parts) &&
                    parts.GetArrayLength() > 0)
                {
                    return parts[0].GetProperty("text").GetString() ?? "Yanit alinamadi.";
                }
            }
            catch (Exception ex)
            {
                lastError = ex.Message;
            }
        }

        return string.IsNullOrWhiteSpace(lastError)
            ? "Gemini su an yanit veremiyor."
            : $"Gemini baglanti hatasi: {lastError}";
    }
}
