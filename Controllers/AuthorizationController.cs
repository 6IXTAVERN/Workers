using Microsoft.AspNetCore.Mvc;

namespace Workers.Controllers;

public class AuthorizationController : Controller
{
    // GET
    public IActionResult Register()
    {
        return View();
    }
}