using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Abstract
{
  public interface IAuthService
  {
    IDataResult<User> Login(UserForLoginDto userForLoginDto);
    IResult UserExists(string email);
  }
}
