using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using FutureCafe.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutureCafe.Web.Controllers
{
  public class ProductController : Controller
  {
    IProductService _productService;
    ICategoryService _categoryService;
    IWebHostEnvironment _webHostEnvironment;

    public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
      _productService = productService;
      _categoryService = categoryService;
      _webHostEnvironment = webHostEnvironment;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var productList = await _productService.GetListAsync<ProductViewDto>(null, null, "ProductCategory.Category");

      if (productList.Data == null)
        return View("Error", new ErrorViewModel { ErrorMessage = productList.Message });

      return View(productList.Data);
    }

    //CREATE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
      PopulateCategoryDropDownList();
      return View();
    }

    [Authorize(Roles = "Admin")]
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

      await SaveProductImage(productDto);

      await _productService.AddAsync(productDto);
      await _productService.SaveAsync();

      return RedirectToAction("Index");
    }


    //EDIT
    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(ProductCreateEditDto productDto)
    {
      if (productDto == null) { return RedirectToAction("Index"); }

      //validate
      var validationResult = _productService.Validate(productDto);
      if (validationResult.Data.IsValid == false && validationResult.Data.Errors.Select(x => x.PropertyName).ToList().Any(x => x.Equals("ProductBarcodNo")) == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        PopulateCategoryDropDownList();
        return View(productDto);
      }

      if (productDto.ImageUrl != null) DeleteProductImage(productDto.ImageUrl);
      await SaveProductImage(productDto);
      _productService.Update(productDto);

      await _productService.SaveAsync();

      return RedirectToAction("Index");

    }

    //DETAIL
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var product = await _productService.FindByIdAsync<ProductDetailDto>(id);

      if (product == null)
      { return RedirectToAction("Index"); }

      return View(product.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var product = await _productService.FindByIdAsync<Product>(id);

      if (product != null)
      {

        if (product.Data.ImageUrl != null) DeleteProductImage(product.Data.ImageUrl);
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

    private async Task SaveProductImage(ProductCreateEditDto productDto)
    {
      if (productDto.ImageFile != null && productDto.ImageFile.Length > 0)
      {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(productDto.ImageFile.FileName);
        var fileExtension = Path.GetExtension(productDto.ImageFile.FileName);

        var fileName = fileNameWithoutExtension + "_" + DateTime.Now.Ticks.ToString() + fileExtension;
        var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageUploads", fileName);

        using (var stream = new FileStream(savePath, FileMode.Create))
        {
          await productDto.ImageFile.CopyToAsync(stream);
        }
        productDto.ImageUrl = "/ImageUploads/" + fileName;
      }
      else
        productDto.ImageUrl = "/imgs/canteen_default.jpg";
    }
    private void DeleteProductImage(string imageUrl)
    {
      imageUrl = imageUrl.Replace("/ImageUploads/", "");
      var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageUploads", imageUrl);
      if (System.IO.File.Exists(filePath))
      {
        System.IO.File.Delete(filePath);
      }
    }

  }
}
