using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoddingAssessmentProject.Web.Models;
using Microsoft.AspNetCore.Authorization;
using CoddingAssessmentProject.Services.Attributes;
using Microsoft.AspNetCore.Identity;

namespace CoddingAssessmentProject.Web.Controllers;

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
    [Authorize(Roles = "User")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
