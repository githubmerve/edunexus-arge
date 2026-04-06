using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class AiAssistantService : IAiAssistantService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenAiOptions _options;
    private readonly GeminiOptions _geminiOptions;
    private readonly IOllamaService _ollamaService;
    private readonly IGeminiService _geminiService;

    public AiAssistantService(
        IHttpClientFactory httpClientFactory,
        IOptions<OpenAiOptions> options,
        IOptions<GeminiOptions> geminiOptions,
        IOllamaService ollamaService,
        IGeminiService geminiService)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _geminiOptions = geminiOptions.Value;
        _ollamaService = ollamaService;
        _geminiService = geminiService;
    }

    public async Task<AiChatResponse> AskAsync(AiChatRequest request, CancellationToken cancellationToken = default)
    {
        var ollamaResponse = await _ollamaService.AskAsync(request, cancellationToken);
        if (ollamaResponse is not null)
        {
            return ollamaResponse;
        }

        // Try Gemini First if API Key is available
        if (!string.IsNullOrWhiteSpace(_geminiOptions.ApiKey))
        {
            var geminiAnswer = await _geminiService.GenerateContentAsync(request.Message, request.History);
            if (!string.IsNullOrWhiteSpace(geminiAnswer))
            {
                return new AiChatResponse
                {
                    Answer = geminiAnswer,
                    AnsweredAtUtc = DateTime.UtcNow,
                    UsedWebSearch = false
                };
            }
        }

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            return new AiChatResponse
            {
                Answer = "Yerel model şu anda erişilebilir değil ve OpenAI API anahtarı da tanımlı görünmüyor. Bu nedenle istemci tarafındaki yerel bilgi tabanı yedek olarak kullanılacak.",
                AnsweredAtUtc = DateTime.UtcNow,
                UsedWebSearch = false
            };
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _options.ApiKey);

        var inputItems = new List<object>
        {
            new
            {
                role = "system",
                content = new object[]
                {
                    new
                    {
                        type = "input_text",
                        text = """
                               Sen Bursa Uludağ Üniversitesi için çalışan bir dijital kampüs asistanısın.
                               Yanıtlarını doğal ve düzgün Türkçe ile ver.
                               Yönetim Bilişim Sistemleri, İşletme, Uluslararası Ticaret, öğrenci süreçleri,
                               mazeretler, kampüs hizmetleri ve genel üniversite yönlendirmeleri hakkında yardımcı ol.
                               Kullanıcı güncel bilgi istediğinde web araması sonuçlarını kullan ve emin olmadığın konularda bunu belirt.
                               """
                    }
                }
            }
        };

        foreach (var turn in request.History.TakeLast(6))
        {
            inputItems.Add(new
            {
                role = turn.Role == "assistant" ? "assistant" : "user",
                content = new object[]
                {
                    new
                    {
                        type = "input_text",
                        text = turn.Text
                    }
                }
            });
        }

        inputItems.Add(new
        {
            role = "user",
            content = new object[]
            {
                new
                {
                    type = "input_text",
                    text = request.Message
                }
            }
        });

        var payload = new
        {
            model = _options.Model,
            input = inputItems.ToArray(),
            tools = _options.UseWebSearch
                ? new object[]
                {
                    new
                    {
                        type = "web_search"
                    }
                }
                : Array.Empty<object>()
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _options.Endpoint)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json")
        };

        using var response = await client.SendAsync(httpRequest, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        using var document = JsonDocument.Parse(content);

        var answer = document.RootElement.TryGetProperty("output_text", out var outputTextElement)
            ? outputTextElement.GetString() ?? "Şu an yanıt üretemedim."
            : "Şu an yanıt üretemedim.";

        var citations = new List<AiCitation>();
        if (document.RootElement.TryGetProperty("sources", out var sourcesElement) &&
            sourcesElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in sourcesElement.EnumerateArray())
            {
                citations.Add(new AiCitation
                {
                    Title = item.TryGetProperty("title", out var title) ? title.GetString() ?? "Kaynak" : "Kaynak",
                    Url = item.TryGetProperty("url", out var url) ? url.GetString() ?? string.Empty : string.Empty
                });
            }
        }

        return new AiChatResponse
        {
            Answer = answer,
            AnsweredAtUtc = DateTime.UtcNow,
            UsedWebSearch = _options.UseWebSearch,
            Sources = citations.Where(x => !string.IsNullOrWhiteSpace(x.Url)).ToList()
        };
    }
}
