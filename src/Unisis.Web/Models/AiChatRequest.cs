namespace Unisis.Web.Models;

public class AiChatRequest
{
    public string UserId { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<AiChatTurn> History { get; set; } = [];
}
