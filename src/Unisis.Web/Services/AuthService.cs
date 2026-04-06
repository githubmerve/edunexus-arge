using Unisis.Web.Models;

namespace Unisis.Web.Services;

public class AuthService : IAuthService
{
    private readonly IFirebaseAuthService _firebaseAuthService;

    private static readonly List<AppUser> Users =
    [
        new AppUser
        {
            StudentNumber = "132230015",
            Password = "merve",
            FullName = "Merve Suba\u015f\u0131",
            Role = "Student",
            FirebaseEmail = "132230015@inegol-chat-system.local"
        },
        new AppUser
        {
            StudentNumber = "132530025",
            Password = "berkay",
            FullName = "Recep Berkay \u00dcnsal",
            Role = "Student",
            FirebaseEmail = "132530025@inegol-chat-system.local"
        },
        new AppUser
        {
            StudentNumber = "132530038",
            Password = "sena",
            FullName = "Sena Do\u011fan",
            Role = "Student",
            FirebaseEmail = "132530038@inegol-chat-system.local"
        },
        new AppUser
        {
            StudentNumber = "132430002",
            Password = "mustafa",
            FullName = "Muhammed Mustafa Kara",
            Role = "Student",
            FirebaseEmail = "132430002@inegol-chat-system.local"
        }
    ];

    public AuthService(IFirebaseAuthService firebaseAuthService)
    {
        _firebaseAuthService = firebaseAuthService;
    }

    public async Task<AppUser?> ValidateCredentialsAsync(
        string userCode,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = Users.FirstOrDefault(x => x.StudentNumber == userCode);
        if (user is null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(user.FirebaseEmail))
        {
            var firebaseResult = await _firebaseAuthService.SignInAsync(
                user.FirebaseEmail,
                password,
                cancellationToken);

            if (firebaseResult is not null)
            {
                return new AppUser
                {
                    StudentNumber = user.StudentNumber,
                    Password = user.Password,
                    FullName = user.FullName,
                    Role = user.Role,
                    FirebaseEmail = firebaseResult.Email,
                    FirebaseIdToken = firebaseResult.IdToken
                };
            }
        }

        return user.Password == password ? user : null;
    }

    public AppUser? GetByStudentNumber(string userCode)
    {
        return Users.FirstOrDefault(x => x.StudentNumber == userCode);
    }

    public IReadOnlyList<AppUser> GetSampleUsers()
    {
        return Users;
    }
}
