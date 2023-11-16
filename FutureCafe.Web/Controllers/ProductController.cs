using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using FutureCafe.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FutureCafe.Web.Controllers
{
  public class ProductController : Controller
  {
    IProductService _productService;
    ICategoryService _categoryService;
    public ProductController(IProductService productService, ICategoryService categoryService)
    {
      _productService = productService;
      _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
      var productList = await _productService.GetListAsync<ProductViewDto>(null, null, "ProductCategory.Category");

      if (productList.Data == null)
        return View("Error", new ErrorViewModel { ErrorMessage = productList.Message });

      return View(productList.Data);
    }

    //CREATE
    [HttpGet]
    public IActionResult Create()
    {
      PopulateCategoryDropDownList();
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateEditDto productDto)
    {
      if (productDto == null) { return RedirectToAction("Index"); }

      //validate
      var validationResult = _productService.Validate(productDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        PopulateCategoryDropDownList();
        return View(productDto);
      }
      await _productService.AddAsync(productDto);
      await _productService.SaveAsync();

      return RedirectToAction("Index");
    }

    //EDIT
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var product = await _productService.FindByIdAsync<ProductCreateEditDto>(id);

      if (product == null)
      { return RedirectToAction("Index"); }

      PopulateCategoryDropDownList();

      return View(product.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductCreateEditDto productDto)
    {
      if (productDto == null) { return RedirectToAction("Index"); }

      //validate
      var validationResult = _productService.Validate(productDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        PopulateCategoryDropDownList();
        return View(productDto);
      }
      _productService.Update(productDto);
      await _productService.SaveAsync();

      return RedirectToAction("Index");

    }

    //DETAIL
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var product = await _productService.FindByIdAsync<ProductDetailDto>(id);

      if (product == null)
      { return RedirectToAction("Index"); }

      PopulateCategoryDropDownList();

      return View(product.Data);
    }

    //DELETE
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var product = await _productService.FindByIdAsync<ProductDetailDto>(id);

      if (product == null)
      { return RedirectToAction("Index"); }

      return View(product.Data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var product = await _productService.FindByIdAsync<Product>(id);

      if (product != null)
      {
        _productService.Delete(product.Data);
        await _productService.SaveAsync();
        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }
    private void PopulateCategoryDropDownList(object? selectedCategory = null)
    {
      var categories = _categoryService.GetList<Category>(orderBy: q => q.OrderBy(x => x.Name));
      ViewBag.AvailableCategories = new SelectList(categories.Data, "Id", "Name", selectedCategory);
    }
  }
}
