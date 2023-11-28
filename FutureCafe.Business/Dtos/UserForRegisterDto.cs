using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class UserForRegisterDto
  {
    [DisplayName("Kullanıcı Adı")]
    public string Email { get; set; }
    [DisplayName("Şifre")]
    public string Password { get; set; }
    [DisplayName("Şifre Tekrar")]
    public string PasswordConfirm { get; set; }
    [DisplayName("İsim")]
    public string FirstName { get; set; }
    [DisplayName("Soyisim")]
    public string LastName { get; set; }
  }
}
