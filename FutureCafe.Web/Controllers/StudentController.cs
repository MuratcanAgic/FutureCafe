using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutureCafe.Web.Controllers
{
  public class StudentController : Controller
  {
    IStudentService _studentService;
    ISchoolClassService _schoolClassService;
    public StudentController(IStudentService studentService, ISchoolClassService schoolClassService)
    {
      _studentService = studentService;
      _schoolClassService = schoolClassService;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var list = await _studentService.GetListAsync<StudentViewDto>();

      /*   if (list.Data == null)
           return View("Error", new ErrorViewModel { ErrorMessage = list.Message });*/

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
      await _studentService.AddAsync(studentDto);
      await _studentService.SaveAsync();

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
      _studentService.Update(studentDto);
      await _studentService.SaveAsync();

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
        _studentService.Delete(student.Data);
        await _studentService.SaveAsync();
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


      _studentService.BanUpdate(Convert.ToInt32(stdId), banDto);
      await _studentService.SaveAsync();

      return RedirectToAction("Index");
    }


    private void PopulateSchoolClassDropDownList(object? selectedCategory = null)
    {
      var schoolClasses = _schoolClassService.GetList<SchoolClass>(orderBy: q => q.OrderBy(x => x.Name));
      ViewBag.SchoolClasses = new SelectList(schoolClasses.Data, "Id", "Name", selectedCategory);
    }
  }
}
