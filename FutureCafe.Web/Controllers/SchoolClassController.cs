using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace FutureCafe.Web.Controllers
{
  public class SchoolClassController : Controller
  {
    private readonly ISchoolClassService _schoolClassService;
    private readonly IToastNotification _toastNotification;
    public SchoolClassController(ISchoolClassService schoolClassService, IToastNotification toastNotification)
    {
      _schoolClassService = schoolClassService;
      _toastNotification = toastNotification;
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var list = await _schoolClassService.GetListAsync<SchoolClassViewDto>();
      return View(list.Data);
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
    public async Task<IActionResult> Create(SchoolClassCreateEditDto schoolClassDto)
    {
      if (schoolClassDto == null) { RedirectToAction("Index"); }

      //validate
      var validationResult = _schoolClassService.Validate(schoolClassDto);

      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        return View(schoolClassDto);
      }

      var addResult = await _schoolClassService.AddAsync(schoolClassDto);
      var saveResult = await _schoolClassService.SaveAsync();

      if (addResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotCreated);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataCreated);

      return RedirectToAction("Index");
    }
    //EDIT
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync<SchoolClassCreateEditDto>(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(SchoolClassCreateEditDto schoolClassDto)
    {
      if (schoolClassDto == null) { return RedirectToAction("Index"); }

      //var schoolClass = _schoolClassService.MapDtoToEntity<SchoolClassCreateEditDto, SchoolClass>(schoolClassDto);

      //validate
      var validationResult = _schoolClassService.Validate(schoolClassDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        return View(schoolClassDto);
      }

      var updateResult = _schoolClassService.Update(schoolClassDto);
      var saveResult = await _schoolClassService.SaveAsync();

      if (updateResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotUpdated);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataUpdated);

      return RedirectToAction("Index");
    }
    //DETAIL
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync<SchoolClassViewDto>(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }

    //DELETE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync<SchoolClassViewDto>(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var deleteResult = _schoolClassService.DeleteById(id);

      var saveResult = await _schoolClassService.SaveAsync();

      if (deleteResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotDeleted);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataDeleted);

      return RedirectToAction("Index");
    }
  }
}
