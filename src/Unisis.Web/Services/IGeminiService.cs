using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IGeminiService
{
    Task<string> GenerateContentAsync(string prompt, IEnumerable<AiChatTurn>? history = null);
}
