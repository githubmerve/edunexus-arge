using Microsoft.AspNetCore.Mvc;
using Unisis.Web.Extensions;
using Unisis.Web.Models;
using Unisis.Web.Services;

namespace Unisis.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public AccountController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.IsAuthenticated())
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            model.ErrorMessage = "Lutfen bilgilerinizi kontrol edin.";
            return View(model);
        }

        var user = await _authService.ValidateCredentialsAsync(
            model.UserCode,
            model.Password,
            cancellationToken);

        if (user is null)
        {
            model.ErrorMessage = "Kullanici kodu veya sifre hatali.";
            return View(model);
        }

        await _userRepository.SaveUserProfileAsync(
            CreateUserProfile(user),
            cancellationToken);

        HttpContext.Session.SetCurrentUser(user);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }

    private static UserProfile CreateUserProfile(AppUser user)
    {
        var parsedRole = Enum.TryParse<UserRole>(user.Role, true, out var role)
            ? role
            : UserRole.Student;

        return new UserProfile
        {
            Id = user.StudentNumber,
            FirebaseUid = user.FirebaseEmail ?? string.Empty,
            UniversityId = user.StudentNumber,
            FullName = user.FullName,
            Email = user.FirebaseEmail ?? $"{user.StudentNumber}@campus.local",
            Role = parsedRole,
            IsActive = true
        };
    }
}
