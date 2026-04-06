using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IAiAssistantService
{
    Task<AiChatResponse> AskAsync(AiChatRequest request, CancellationToken cancellationToken = default);
}
