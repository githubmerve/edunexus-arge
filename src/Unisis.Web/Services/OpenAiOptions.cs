namespace Unisis.Web.Services;

public class OpenAiOptions
{
    public const string SectionName = "OpenAI";

    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "gpt-5-mini";
    public string Endpoint { get; set; } = "https://api.openai.com/v1/responses";
    public bool UseWebSearch { get; set; } = true;
}
