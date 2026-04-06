using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Unisis.Web.Services;

namespace Unisis.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IQrLoginService _qrLoginService;

    public AuthController(IQrLoginService qrLoginService)
    {
        _qrLoginService = qrLoginService;
    }

    [HttpPost("qr/generate")]
    public async Task<IActionResult> GenerateQr(
        [FromQuery] string studentId,
        [FromQuery] string deviceId,
        CancellationToken cancellationToken)
    {
        var rawToken = await _qrLoginService.CreateLoginTokenAsync(studentId, deviceId, cancellationToken);
        using var generator = new QRCodeGenerator();
        using var data = generator.CreateQrCode(rawToken, QRCodeGenerator.ECCLevel.Q);
        var qrBytes = new PngByteQRCode(data).GetGraphic(20);

        return Ok(new
        {
            token = rawToken,
            expiresInSeconds = 45,
            qrImageBase64 = Convert.ToBase64String(qrBytes)
        });
    }

    [HttpPost("qr/validate")]
    public async Task<IActionResult> ValidateQr(
        [FromBody] string rawToken,
        CancellationToken cancellationToken)
    {
        var isValid = await _qrLoginService.ValidateLoginTokenAsync(rawToken, cancellationToken);
        return Ok(new { success = isValid });
    }
}
