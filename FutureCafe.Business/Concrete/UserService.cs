using FutureCafe.Business.Abstract;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Concrete
{
  public class UserService : IUserService
  {
    IUserDal _userDal;

    public UserService(IUserDal userDal)
    {
      _userDal = userDal;
    }

    public List<OperationClaim> GetClaims(User user)
    {
      try
      {
        return _userDal.GetClaims(user);
      }
      catch (Exception)
      {

        throw;
      }

    }
    public void Add(User user)
    {
      try
      {
        _userDal.Add(user);
      }
      catch (Exception)
      {

        throw;
      }

    }
    public User GetByMail(string email)
    {
      try
      {
        return _userDal.Get(u => u.Email == email);
      }
      catch (Exception)
      {

        throw;
      }

    }
  }
}
