using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class StudentViewDto
  {
    public int Id { get; set; }

    [DisplayName("Öğrenci Adı")]
    public string Name { get; set; }

    [DisplayName("Öğrenci	Soyadı")]
    public string SurName { get; set; }

    [DisplayName("Öğrenci	Kart Numarası")]
    public string CardNumber { get; set; }
  }
}
