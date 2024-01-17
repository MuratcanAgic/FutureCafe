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
  public class StudentController : Controller
  {
    private readonly IStudentService _studentService;
    private readonly ISchoolClassService _schoolClassService;
    private readonly IToastNotification _toastNotification;
    public StudentController(IStudentService studentService, ISchoolClassService schoolClassService, IToastNotification toastNotification)
    {
      _studentService = studentService;
      _schoolClassService = schoolClassService;
      _toastNotification = toastNotification;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var list = await _studentService.GetListAsync<StudentViewDto>();

      return View(list.Data);
    }

    [Authorize(Roles = "Admin")]
    //CREATE
    [HttpGet]
    public IActionResult Create()
    {
      PopulateSchoolClassDropDownList();
      return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(StudentCreateEditDto studentDto)
    {
      if (studentDto == null) { return RedirectToAction("Index"); }


      //validate
      var validationResult = _studentService.Validate(studentDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        PopulateSchoolClassDropDownList();
        return View(studentDto);
      }
      var addResult = await _studentService.AddAsync(studentDto);
      var saveResult = await _studentService.SaveAsync();

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

      var student = await _studentService.FindByIdAsync<StudentCreateEditDto>(id);

      if (student == null)
      { return RedirectToAction("Index"); }

      PopulateSchoolClassDropDownList();

      return View(student.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(StudentCreateEditDto studentDto)
    {
      if (studentDto == null) { return RedirectToAction("Index"); }

      //validate
      var validationResult = _studentService.Validate(studentDto);
      if (validationResult.Data.IsValid == false && validationResult.Data.Errors.Select(x => x.PropertyName).ToList().Any(x => x.Equals("CardNumber")) == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        PopulateSchoolClassDropDownList();
        return View(studentDto);
      }
      var updateResult = _studentService.Update(studentDto);
      var saveResult = await _studentService.SaveAsync();

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

      var studentList = await _studentService.GetListAsync<StudentDetailDto>(null
        , null, "SchoolClass");
      var student = studentList.Data.FirstOrDefault(x => x.Id == id);

      if (student == null)
      { return RedirectToAction("Index"); }

      return View(student);
    }

    //DELETE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var student = await _studentService.FindByIdAsync<StudentViewDto>(id);

      if (student == null)
      { return RedirectToAction("Index"); }

      return View(student.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var student = await _studentService.FindByIdAsync<Student>(id);

      if (student != null)
      {
        var deleteResult = _studentService.Delete(student.Data);
        var saveResult = await _studentService.SaveAsync();

        if (deleteResult.Success == false || saveResult.Success == false)
          _toastNotification.AddErrorToastMessage(Messages.DataNotDeleted);
        else
          _toastNotification.AddSuccessToastMessage(Messages.DataDeleted);

        return RedirectToAction("Index");
      }
      return RedirectToAction("Index");
    }

    //BAN
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Ban(int id)
    {
      var banList = await _studentService.GetProductsByCagegoryToBanAsync(id);
      TempData["studentId"] = id;
      return View(banList.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Ban(List<ProductBanDto> banDto)
    {
      var stdId = TempData["studentId"];
      if (banDto == null) { return RedirectToAction("Index"); }


      var banResult = _studentService.BanUpdate(Convert.ToInt32(stdId), banDto);
      var saveResult = await _studentService.SaveAsync();

      if (banResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotUpdated);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataUpdated);

      return RedirectToAction("Index");
    }


    private void PopulateSchoolClassDropDownList(object? selectedCategory = null)
    {
      var schoolClasses = _schoolClassService.GetList<SchoolClass>(orderBy: q => q.OrderBy(x => x.Name));
      ViewBag.SchoolClasses = new SelectList(schoolClasses.Data, "Id", "Name", selectedCategory);
    }
  }
}
