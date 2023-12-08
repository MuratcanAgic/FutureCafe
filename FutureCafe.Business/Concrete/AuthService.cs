using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Core.Utilities.Security;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Concrete
{
  public class AuthService : IAuthService
  {
    private IUserService _userService;

    public AuthService(IUserService userService)
    {
      _userService = userService;
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
      var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
      if (userToCheck == null)
      {
        return new ErrorDataResult<User>("Kullanıcı bulunamadı");
      }

      if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
      {
        return new ErrorDataResult<User>("Parola hatası");
      }

      return new SuccessDataResult<User>(userToCheck, "Başarılı giriş");
    }

    public IResult UserExists(string email)
    {
      if (_userService.GetByMail(email) != null)
      {
        return new ErrorResult("Kullanıcı mevcut");
      }
      return new SuccessResult();
    }
  }
}
