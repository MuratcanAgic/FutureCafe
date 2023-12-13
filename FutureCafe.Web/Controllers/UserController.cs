using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FutureCafe.Web.Controllers
{
  public class UserController : Controller
  {
    IUserService _userService;
    IAuthService _authService;
    IOperationClaimService _operationClaimService;
    public UserController(IUserService userService, IAuthService authService, IOperationClaimService operationClaimService)
    {
      _userService = userService;
      _authService = authService;
      _operationClaimService = operationClaimService;
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var userList = await _userService.GetListAsync<UserForViewDto>(null, null, "UserOperationClaims.OperationClaim");
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
      PopulateRolesDropDownList();
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
        PopulateRolesDropDownList();
        return View(userDto);
      }
      await _userService.AddAsync(userDto);
      await _userService.SaveAsync();

      return RedirectToAction("Index");
    }


    //EDIT
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var user = await _userService.FindByIdAsync<UserForRegisterDto>(id);

      if (user == null)
      { return RedirectToAction("Index"); }

      PopulateRolesDropDownList();

      return View(user.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(UserForRegisterDto usertDto)
    {
      if (usertDto == null) { return RedirectToAction("Index"); }

      //validate
      var validationResult = _userService.Validate(usertDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        PopulateRolesDropDownList();
        return View(usertDto);
      }

      _userService.Update(usertDto);

      await _userService.SaveAsync();

      return RedirectToAction("Index");
    }

    //DELETE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var user = await _userService.GetAsync<UserForViewDto>(x => x.Id == id, "UserOperationClaims.OperationClaim");

      if (user == null)
      { return RedirectToAction("Index"); }

      return View(user.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var user = await _userService.FindByIdAsync<User>(id);

      if (user != null)
      {
        _userService.Delete(user.Data);
        await _userService.SaveAsync();
        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }

    private void PopulateRolesDropDownList(object? selectedClaim = null)
    {
      var claims = _operationClaimService.GetList<OperationClaim>(orderBy: q => q.OrderBy(x => x.Name));
      ViewBag.AvailableClaims = new SelectList(claims.Data, "Id", "Name", selectedClaim);
    }
  }
}
