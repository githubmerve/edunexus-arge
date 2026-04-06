using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IAuthService
{
    Task<AppUser?> ValidateCredentialsAsync(string userCode, string password, CancellationToken cancellationToken = default);
    AppUser? GetByStudentNumber(string userCode);
    IReadOnlyList<AppUser> GetSampleUsers();
}
