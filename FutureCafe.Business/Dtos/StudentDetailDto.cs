using FutureCafe.Entities.Concrete;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class StudentDetailDto
  {

    public int Id { get; set; }

    [DisplayName("Öğrenci Adı")]
    public string Name { get; set; }

    [DisplayName("Öğrenci	Soyadı")]
    public string SurName { get; set; }

    [DisplayName("Öğrenci	Kart Numarası")]
    public string CardNumber { get; set; }

    [DisplayName("Öğrenci	Sınıfı")]
    public SchoolClass SchoolClass { get; set; }

    [DisplayName("Öğrenci Okul Numarası")]
    public string StudentSchoolNumber { get; set; }
  }
}
