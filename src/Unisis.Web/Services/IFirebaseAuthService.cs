using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IFirebaseAuthService
{
    Task<FirebaseSignInResponse?> SignInAsync(string email, string password, CancellationToken cancellationToken = default);
}
