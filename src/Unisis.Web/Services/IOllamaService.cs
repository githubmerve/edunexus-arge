using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IOllamaService
{
    Task<AiChatResponse?> AskAsync(AiChatRequest request, CancellationToken cancellationToken = default);
}
