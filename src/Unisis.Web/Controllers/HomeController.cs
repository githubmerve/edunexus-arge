using Microsoft.AspNetCore.Mvc;
using Unisis.Web.Extensions;
using Unisis.Web.Models;

namespace Unisis.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        if (!HttpContext.Session.IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new HomeDashboardViewModel());
    }

    [HttpGet]
    public IActionResult Student()
    {
        if (!HttpContext.Session.IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new HomeDashboardViewModel());
    }

    [HttpGet]
    public IActionResult Academician()
    {
        if (!HttpContext.Session.IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new HomeDashboardViewModel());
    }

    [HttpGet]
    public IActionResult Assistant()
    {
        if (!HttpContext.Session.IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new HomeDashboardViewModel());
    }
}
