using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FutureCafe.Entities.Concrete
{
  public class User : BaseEntity, IEntity
  {
    [DisplayName("Kullanıcı Adı")]
    [Required(ErrorMessage = "{0} boş geçilemez")]
    [StringLength(50, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }

    [DisplayName("Kullanıcı Soyadı")]
    [Required(ErrorMessage = "{0} boş geçilemez")]
    [StringLength(50, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    [MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string SurName { get; set; }

    [Required(ErrorMessage = "{0} boş geçilemez")]
    [DisplayName("Kullanıcı E-mail")]
    [EmailAddress(ErrorMessage = "Hatalı e-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} boş geçilemez")]
    [DisplayName("Kullanıcı Şifre")]
    //[PasswordPropertyText]
    [StringLength(50, ErrorMessage = "{0} {1} karakterden fazla olamaz")]
    [MinLength(3, ErrorMessage = "{0} {1} karakterden az olamaz")]
    public string Passowrd { get; set; }

    [ScaffoldColumn(false)]
    [DisplayName("Kullanıcı Ad Soyad")]
    public string NameSurname => Name + " " + SurName;

    public int AuthorityId { get; set; }
    public Authority? Authority { get; set; }
  }
}
