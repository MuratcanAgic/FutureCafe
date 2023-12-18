using FutureCafe.Entities.Concrete;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class UserForRegisterDto
  {
    public int Id { get; set; }
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
    [DisplayName("Roller")]

    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public bool? Status { get; set; }
    [DisplayName("Roller")]
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    public IEnumerable<int> SelectClaimIds { get; set; }
  }
}
