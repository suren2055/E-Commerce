
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WEB.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<IActionResult> SignIn(string returnUrl)
    {
        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
    }

    public async Task<IActionResult> Signout()
    {
        return Ok("Success");
    }
}