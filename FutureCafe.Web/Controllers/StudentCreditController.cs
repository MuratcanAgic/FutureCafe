using FutureCafe.Business.Abstract;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class StudentCreditController : Controller
  {
    IStudentService _studentService;

    public StudentCreditController(IStudentService studentService)
    {
      _studentService = studentService;
    }

    public IActionResult Load()
    {
      return View();
    }
    public IActionResult Show()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> FindStudent(string studentCardNumber)
    {
      var student = await _studentService.GetAsync<Student>(e => e.CardNumber == studentCardNumber);

      if (student.Data != null)
      {
        return Json(new { success = true, studentFullName = student.Data.NameSurname });
      }
      else
      {
        return Json(new { success = false });
      }
    }

    [HttpPost]
    public async Task<IActionResult> LoadMoney(decimal loadAmount, string studentCardNumber)
    {
      var student = await _studentService.GetAsync<Student>(e => e.CardNumber == studentCardNumber, "StudentCredit.Credit");

      if (student.Data != null && loadAmount != 0)
      {
        try
        {
          var lastAmount = student.Data.StudentCredit == null || student.Data.StudentCredit.LastOrDefault() == null ? 0 : student.Data.StudentCredit.LastOrDefault().Credit.Amount;
          var newCreditAmount = new Credit { Amount = loadAmount + lastAmount };

          student.Data.StudentCredit.Add(new StudentCredit { Credit = newCreditAmount, Student = student.Data });

          _studentService.Update(student.Data);
          await _studentService.SaveAsync();

          return Json(new { success = true, studentFullName = student.Data.NameSurname, newCredit = newCreditAmount.Amount });
        }
        catch (Exception)
        {
          return Json(new { success = false });
        }

      }
      else
      {
        return Json(new { success = false });
      }
    }

    [HttpGet]
    public async Task<IActionResult> ShowMoney(string studentCardNumber)
    {
      var student = await _studentService.GetAsync<Student>(e => e.CardNumber == studentCardNumber, "StudentCredit.Credit");

      if (student != null)
      {
        try
        {
          var lastAmount = student.Data.StudentCredit == null || student.Data.StudentCredit.LastOrDefault() == null ? 0 : student.Data.StudentCredit.LastOrDefault().Credit.Amount;

          return Json(new { success = true, studentFullName = student.Data.NameSurname, credit = lastAmount });
        }
        catch (Exception)
        {
          return Json(new { success = false });
        }

      }
      else
      {
        return Json(new { success = false });
      }
    }
  }
}
