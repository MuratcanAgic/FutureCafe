using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FutureCafe.Web.Controllers
{
  public class AuthController : Controller
  {
    private IAuthService _authService;
    private IUserService _userService;
    public AuthController(IAuthService authService, IUserService userService)
    {
      _authService = authService;
      _userService = userService;
    }

    public IActionResult Login()
    {
      return View();
    }
    public IActionResult AccessDenied()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
      var userToLogin = _authService.Login(userForLoginDto);
      if (!userToLogin.Success)
      {
        return BadRequest(userToLogin.Message);
      }
      var claimsDb = _userService.GetClaims(userToLogin.Data);

      var claims = new List<Claim>()
      {
        new Claim(ClaimTypes.NameIdentifier,"1"),
        new Claim(ClaimTypes.Name,userToLogin.Data.FirstName),
        new Claim(ClaimTypes.Surname,userToLogin.Data.LastName),
        new Claim(ClaimTypes.Email,userToLogin.Data.Email)
      };

      if (claimsDb != null && claimsDb.Count != 0)
      {
        foreach (var claim in claimsDb)
        {
          claims.Add(new Claim(ClaimTypes.Role, claim.Name));
        }
      }

      var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
      var authProperties = new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddMinutes(5) };

      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple, authProperties);

      return RedirectToAction("Index", "Trade");
    }

    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction("Login", "Auth");
    }
  }
}
