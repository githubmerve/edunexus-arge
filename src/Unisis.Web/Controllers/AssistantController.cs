using Microsoft.AspNetCore.Mvc;
using Unisis.Web.Models;
using Unisis.Web.Services;

namespace Unisis.Web.Controllers;

[ApiController]
[Route("api/assistant")]
public class AssistantController : ControllerBase
{
    private readonly IAiAssistantService _assistantService;

    public AssistantController(IAiAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(
        [FromBody] AiChatRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserId) ||
            string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("UserId ve message zorunludur.");
        }

        var response = await _assistantService.AskAsync(request, cancellationToken);
        return Ok(response);
    }
}
