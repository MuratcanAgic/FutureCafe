using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class AuthController : Controller
  {
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    public IActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public IActionResult Login(UserForLoginDto userForLoginDto)
    {
      var userToLogin = _authService.Login(userForLoginDto);
      if (!userToLogin.Success)
      {
        return BadRequest(userToLogin.Message);
      }
      return View();
    }
  }
}
