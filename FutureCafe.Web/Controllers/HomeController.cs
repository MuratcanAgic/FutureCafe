﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
      return View();
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
      return View();
    }

    /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }*/
  }
}