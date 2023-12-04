using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
	public class UserController : Controller
	{
		IUserService _userService;
		IAuthService _authService;
		public UserController(IUserService userService, IAuthService authService)
		{
			_userService = userService;
			_authService = authService;
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

		//CREATE
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Create(UserForRegisterDto userDto)
		{
			if (userDto == null || userDto.Password == null) { RedirectToAction("Index"); }

			//validate
			var validationResult = _userService.Validate(userDto);
			if (validationResult.Data.IsValid == false)
			{
				validationResult.Data.AddToModelState(this.ModelState);
				return View(userDto);
			}

			_authService.Register(userDto, userDto.Password);
			await _userService.SaveAsync();

			return RedirectToAction("Index");
		}
	}
}
