namespace Unisis.Web.Services;

public class OllamaOptions
{
    public const string SectionName = "Ollama";

    public bool Enabled { get; set; } = true;
    public string Endpoint { get; set; } = "http://localhost:11434/api/chat";
    public string Model { get; set; } = "llama3.1:8b";
}
