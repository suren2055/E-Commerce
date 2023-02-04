using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.WEB.Models;

namespace E_Commerce.WEB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Catalog()
    {
        return RedirectToAction("Index","Catalog");
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorVM {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}