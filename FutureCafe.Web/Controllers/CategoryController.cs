using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace FutureCafe.Web.Controllers
{
  public class CategoryController : Controller
  {
    private readonly ICategoryService _categoryService;
    private readonly IToastNotification _toastNotification;
    public CategoryController(ICategoryService categoryService, IToastNotification toastNotification)
    {
      _categoryService = categoryService;
      _toastNotification = toastNotification;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var list = await _categoryService.GetListAsync<CategoryViewDto>();

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
    public async Task<IActionResult> Create(CategoryCreateEditDto categoryDto)
    {
      if (categoryDto == null) { RedirectToAction("Index"); }

      //var category = _categoryService.MapDtoToEntity<CategoryCreateEditDto, Category>(categoryDto);
      //validate
      var validationResult = _categoryService.Validate(categoryDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        return View(categoryDto);
      }

      var addResult = await _categoryService.AddAsync<CategoryCreateEditDto>(categoryDto);
      var saveResult = await _categoryService.SaveAsync();

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

      var category = await _categoryService.FindByIdAsync<CategoryCreateEditDto>(id);

      if (category == null)
      { return RedirectToAction("Index"); }


      return View(category.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(CategoryCreateEditDto categoryDto)
    {
      if (categoryDto == null) { return RedirectToAction("Index"); }

      //var category = _categoryService.MapDtoToEntity<CategoryCreateEditDto, Category>(categoryDto);

      //validate
      var validationResult = _categoryService.Validate(categoryDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        return View(categoryDto);
      }

      var updateResult = _categoryService.Update(categoryDto);
      var saveResult = await _categoryService.SaveAsync();

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

      var category = await _categoryService.FindByIdAsync<CategoryViewDto>(id);

      if (category == null)
      { return RedirectToAction("Index"); }

      return View(category.Data);
    }
    //DELETE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var category = await _categoryService.FindByIdAsync<CategoryViewDto>(id);

      if (category == null)
      { return RedirectToAction("Index"); }

      return View(category.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var deleteResult = _categoryService.DeleteById(id);

      var saveResult = await _categoryService.SaveAsync();

      if (deleteResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotDeleted);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataDeleted);

      return RedirectToAction("Index");
    }
  }
}
