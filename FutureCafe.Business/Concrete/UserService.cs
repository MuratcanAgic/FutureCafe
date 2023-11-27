using AutoMapper;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
  public class UserService : IUserService
  {
    IUserDal _userDal;
    IMapper _mapper;
    public UserService(IUserDal userDal, IMapper mapper)
    {
      _userDal = userDal;
      _mapper = mapper;
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

    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var userList = await _userDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = userList.Select(e => _mapper.Map<User, TDto>(e)).ToList();
        if (dto == null)
        {
          return new ErrorDataResult<IEnumerable<TDto>>(Messages.ListEmpty);
        }
        return new SuccessDataResult<IEnumerable<TDto>>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<IEnumerable<TDto>>(e.Message.ToString());
      }
    }
  }
}
