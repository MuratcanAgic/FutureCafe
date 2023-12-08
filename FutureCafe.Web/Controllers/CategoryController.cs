using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class CategoryController : Controller
  {
    ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var list = await _categoryService.GetListAsync<CategoryViewDto>();

      if (list.Data == null)
        return View("Error", new ErrorViewModel { ErrorMessage = list.Message });

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

      await _categoryService.AddAsync<CategoryCreateEditDto>(categoryDto);
      await _categoryService.SaveAsync();

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

      _categoryService.Update(categoryDto);
      await _categoryService.SaveAsync();

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

      _categoryService.DeleteById(id);

      var saveResult = await _categoryService.SaveAsync();

      if (saveResult.Success == false)
      {
        return View("Error", new ErrorViewModel { ErrorMessage = saveResult.Message });
      }
      return RedirectToAction("Index");
    }
  }
}
