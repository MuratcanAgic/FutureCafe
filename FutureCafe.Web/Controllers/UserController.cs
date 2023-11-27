using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class UserController : Controller
  {
    IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var userList = await _userService.GetListAsync<UserForViewDto>();
      if (userList == null)
      {
        return RedirectToAction("Index", "Trade");
      }
      return View(userList.Data);
    }
  }
}
