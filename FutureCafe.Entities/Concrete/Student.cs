using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FutureCafe.Entities.Concrete
{
  public class Student : BaseEntity, IEntity
  {
    [DisplayName("Öğrenci Adı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(50, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }

    [DisplayName("Öğrenci	Soyadı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(50, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string SurName { get; set; }

    [DisplayName("Öğrenci Fotoğraf Yolu")]
    //[StringLength(500, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string? ImageUrl { get; set; }

    [DisplayName("Öğrenci	Kart Numarası")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(150, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string CardNumber { get; set; }

    [DisplayName("Öğrenci Okul Numarası")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(80, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string? StudentSchoolNumber { get; set; }

    [ScaffoldColumn(false)]
    [DisplayName("Öğrenci Ad Soyad")]
    public string NameSurname => Name + " " + SurName;

    [DisplayName("Öğrenci	Sınıfı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    public int SchoolClassId { get; set; }

    public SchoolClass? SchoolClass { get; set; }

    public virtual ICollection<StudentCredit>? StudentCredit { get; set; }
    public virtual ICollection<StudentCategory>? StudentCategory { get; set; }
  }
}
