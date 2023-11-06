using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using FutureCafe.Web.Models;
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

    public async Task<IActionResult> Index()
    {
      var list = await _categoryService.GetListAsync();

      if (list.Data == null)
        return View("Error", new ErrorViewModel { ErrorMessage = list.Message });

      return View(list.Data);
    }

    //CREATE
    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateEditDto categoryDto)
    {
      if (categoryDto == null) { RedirectToAction("Index"); }

      var category = _categoryService.MapDtoToEntity<CategoryCreateEditDto, Category>(categoryDto);
      //validate
      var validationResult = await _categoryService.ValidateAsync(category.Data);
      if (validationResult.IsValid == false)
      {
        validationResult.AddToModelState(this.ModelState);
        return View(categoryDto);
      }
      await _categoryService.AddAsync(category.Data);
      await _categoryService.SaveAsync();

      return RedirectToAction("Index");
    }
    //EDIT
    public async Task<IActionResult> Edit(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var category = await _categoryService.FindByIdAsync(id);

      if (category == null)
      { return RedirectToAction("Index"); }

      var categoryDto = _categoryService.MapEntityToDto<Category, CategoryCreateEditDto>(category.Data);

      return View(categoryDto.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(CategoryCreateEditDto categoryDto)
    {
      if (categoryDto == null) { return RedirectToAction("Index"); }

      var category = _categoryService.MapDtoToEntity<CategoryCreateEditDto, Category>(categoryDto);

      //validate
      var validationResult = await _categoryService.ValidateAsync(category.Data);
      if (validationResult.IsValid == false)
      {
        validationResult.AddToModelState(this.ModelState);
        return View(categoryDto);
      }

      _categoryService.Update(category.Data);
      await _categoryService.SaveAsync();

      return RedirectToAction("Index");
    }
    //DETAIL
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var category = await _categoryService.FindByIdAsync(id);

      if (category == null)
      { return RedirectToAction("Index"); }

      return View(category.Data);
    }
    //DELETE

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var category = await _categoryService.FindByIdAsync(id);

      if (category == null)
      { return RedirectToAction("Index"); }

      return View(category.Data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      _categoryService.DeleteById(id);
      await _categoryService.SaveAsync();

      return RedirectToAction("Index");
    }
  }
}
