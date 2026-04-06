namespace Unisis.Web.Models;

public class AiChatResponse
{
    public string Answer { get; set; } = string.Empty;
    public DateTime AnsweredAtUtc { get; set; } = DateTime.UtcNow;
    public bool UsedWebSearch { get; set; }
    public List<AiCitation> Sources { get; set; } = [];
}
