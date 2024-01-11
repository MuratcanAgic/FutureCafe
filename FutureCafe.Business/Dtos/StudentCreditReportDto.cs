using FutureCafe.Entities.Concrete;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class StudentCreditReportDto
  {
    [DisplayName("Öğrenci Adı Soyadı")]
    public string NameSurname { get; set; }

    public IEnumerable<StudentCredit> StudentCredit { get; set; }
  }
}
