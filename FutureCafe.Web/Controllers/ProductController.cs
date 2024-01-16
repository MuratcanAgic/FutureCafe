using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace FutureCafe.Web.Controllers
{
  public class ProductController : Controller
  {
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IToastNotification _toastNotification;
    public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, IToastNotification toastNotification)
    {
      _productService = productService;
      _categoryService = categoryService;
      _webHostEnvironment = webHostEnvironment;
      _toastNotification = toastNotification;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var productList = await _productService.GetListAsync<ProductViewDto>(null, null, "ProductCategory.Category");

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

      var addResult = await _productService.AddAsync(productDto);
      var saveResult = await _productService.SaveAsync();

      if (addResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotCreated);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataCreated);

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

      if (productDto.ImageFile != null)
      {
        if (productDto.ImageUrl != null) DeleteProductImage(productDto.ImageUrl);
        await SaveProductImage(productDto);
      }
      var updateResult = _productService.Update(productDto);

      var saveResult = await _productService.SaveAsync();

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

        var deleteResult = _productService.Delete(product.Data);
        var saveResult = await _productService.SaveAsync();

        if (deleteResult.Success == false || saveResult.Success == false)
          _toastNotification.AddErrorToastMessage(Messages.DataNotDeleted);
        else
          _toastNotification.AddSuccessToastMessage(Messages.DataDeleted);

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
        var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageUploads");
        var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageUploads", fileName);

        if (Directory.Exists(folderPath) == false)
        {
          Directory.CreateDirectory(folderPath);
        }

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
