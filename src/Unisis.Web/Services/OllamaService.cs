using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class OllamaService : IOllamaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OllamaOptions _options;

    public OllamaService(
        IHttpClientFactory httpClientFactory,
        IOptions<OllamaOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<AiChatResponse?> AskAsync(AiChatRequest request, CancellationToken cancellationToken = default)
    {
        if (!_options.Enabled)
        {
            return null;
        }

        var client = _httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(90);

        var messages = new List<object>
        {
            new
            {
                role = "system",
                content = """
                          Sen Bursa Uludağ Üniversitesi için çalışan dijital kampüs asistanısın.
                          Yanıtlarını düzgün ve doğal Türkçe ile ver.
                          Yönetim Bilişim Sistemleri, İşletme, Uluslararası Ticaret, öğrenci süreçleri,
                          mazeretler, kampüs yaşamı, akademik yönlendirmeler ve bölüm karşılaştırmaları hakkında yardımcı ol.
                          Cevaplarını açık, mantıklı ve bağlama uygun şekilde sürdür.
                          """
            }
        };

        foreach (var turn in request.History.TakeLast(8))
        {
            messages.Add(new
            {
                role = turn.Role == "assistant" ? "assistant" : "user",
                content = turn.Text
            });
        }

        messages.Add(new
        {
            role = "user",
            content = request.Message
        });

        var payload = new
        {
            model = _options.Model,
            stream = false,
            messages
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _options.Endpoint)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json")
        };

        try
        {
            using var response = await client.SendAsync(httpRequest, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            using var document = JsonDocument.Parse(content);

            var answer = document.RootElement
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            if (string.IsNullOrWhiteSpace(answer))
            {
                return null;
            }

            return new AiChatResponse
            {
                Answer = answer,
                AnsweredAtUtc = DateTime.UtcNow,
                UsedWebSearch = false
            };
        }
        catch
        {
            return null;
        }
    }
}
