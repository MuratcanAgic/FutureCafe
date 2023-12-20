using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Business;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.Core.Utilities.Security;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
  public class UserService : IUserService
  {
    IUserDal _userDal;
    IMapper _mapper;
    IValidator<UserForRegisterDto> _validator;
    IOperationClaimDal _operationClaimDal;
    public UserService(IUserDal userDal, IMapper mapper, IValidator<UserForRegisterDto> validator, IOperationClaimDal operationClaimDal)
    {
      _userDal = userDal;
      _mapper = mapper;
      _validator = validator;
      _operationClaimDal = operationClaimDal;
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

    public IResult Any(Expression<Func<User, bool>> filter)
    {
      try
      {
        var exist = _userDal.Any(filter);

        if (exist == true) return new SuccessResult(Messages.DataExist);

        return new ErrorResult(Messages.DataNotExist);
      }
      catch (Exception e)
      {
        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<UserForRegisterDto> Add(UserForRegisterDto dto)
    {
      try
      {
        var entity = _mapper.Map<UserForRegisterDto, User>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<UserForRegisterDto>(dto, result.Message);
        }
        _userDal.Add(entity);
        return new SuccessDataResult<UserForRegisterDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<UserForRegisterDto>(e.Message);
      }
    }
    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, User>(dto);
        _userDal.Delete(entity);
        return new SuccessDataResult<User>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<User>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _userDal.DeleteById(id);
        return new SuccessDataResult<User>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<User>(e.Message);
      }
    }
    public IDataResult<User> GetByMail(string email)
    {
      try
      {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
      }
      catch (Exception e)
      {
        return new ErrorDataResult<User>(e.Message);
      }

    }
    public IResult Save()
    {
      try
      {
        _userDal.Save();
        return new SuccessDataResult<Student>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _userDal.SaveAsync();
        return new SuccessDataResult<Student>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IResult Update(UserForRegisterDto dto)
    {
      try
      {
        var userList = _userDal.GetList(null, null, "UserOperationClaims.OperationClaim");
        var entity = userList.Where(x => x.Id == dto.Id).FirstOrDefault();

        if (entity != null)
        {
          entity.UserOperationClaims.Clear();

          if (dto.SelectClaimIds != null && dto.SelectClaimIds.Any())
          {
            foreach (var item in dto.SelectClaimIds)
            {
              var operationClaim = _operationClaimDal.FindById(item);
              entity.UserOperationClaims.Add(new UserOperationClaim { OperationClaim = operationClaim, User = entity });
            }
          }
        }
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);
        entity.PasswordHash = passwordHash;
        entity.PasswordSalt = passwordSalt;
        entity.Status = true;

        //business result
        IResult result = BusinessRules.Run();
        if (result != null)
        {
          return new ErrorDataResult<UserForRegisterDto>(dto, result.Message);
        }
        _userDal.Update(entity);
        return new SuccessDataResult<UserForRegisterDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<UserForRegisterDto>(e.Message);
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
    public async Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id) where TDto : class
    {
      try
      {
        var userList = await _userDal.GetListAsync(null, null, "UserOperationClaims.OperationClaim");
        var userClass = userList.Where(x => x.Id == id).FirstOrDefault();

        if (typeof(TDto) == typeof(UserForRegisterDto))
        {
          var userClassDto = _mapper.Map<User, UserForRegisterDto>(userClass);
          userClassDto.SelectClaimIds = userClassDto.UserOperationClaims.Select(x => x.OperationClaimId).ToList();

          return new SuccessDataResult<TDto>(userClassDto as TDto);
        }

        else if (typeof(TDto) == typeof(User))
        {
          return new SuccessDataResult<TDto>(userClass as TDto);
        }
        else
        {
          return new ErrorDataResult<TDto>($"Desteklenmeyen tip: {typeof(TDto)}");
        }
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }


    public async Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<User, bool>> filter, string includeProperties = "")
    {
      try
      {
        var user = await _userDal.GetAsync(filter, includeProperties);
        var userDto = _mapper.Map<User, TDto>(user);
        if (userDto == null)
        {
          return new ErrorDataResult<TDto>(Messages.DataNotFound);
        }
        return new SuccessDataResult<TDto>(userDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message.ToString());
      }
    }
    public async Task<IDataResult<UserForRegisterDto>> AddAsync(UserForRegisterDto dto)
    {
      try
      {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);
        dto.PasswordHash = passwordHash;
        dto.PasswordSalt = passwordSalt;
        dto.Status = true;

        if (dto.SelectClaimIds != null && dto.SelectClaimIds.Any())
        {
          dto.UserOperationClaims = dto.SelectClaimIds
              .Select(claimId => new UserOperationClaim { OperationClaimId = claimId })
              .ToList();
        }

        var entity = _mapper.Map<UserForRegisterDto, User>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<UserForRegisterDto>(dto, result.Message);
        }
        await _userDal.AddAsync(entity);
        return new SuccessDataResult<UserForRegisterDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<UserForRegisterDto>(e.Message);
      }
    }
    public IDataResult<ValidationResult> Validate(UserForRegisterDto dto)
    {
      try
      {
        //var entity = _mapper.Map<User>(dto);

        var validationResult = _validator.Validate(dto);

        if (validationResult == null)
          return new ErrorDataResult<ValidationResult>(Messages.ValidationResultNull);

        return new SuccessDataResult<ValidationResult>(validationResult);

      }
      catch (Exception e)
      {
        return new ErrorDataResult<ValidationResult>(e.Message);
      }
    }
  }
}
