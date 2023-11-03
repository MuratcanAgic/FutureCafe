using FutureCafe.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class SchoolClassController : Controller
  {
    ISchoolClassService _schoolClassService;

    public SchoolClassController(ISchoolClassService schoolClassService)
    {
      _schoolClassService = schoolClassService;
    }

    public IActionResult Index()
    {
      var list = _schoolClassService.GetList();
      return View(list.Data);
    }
  }
}
