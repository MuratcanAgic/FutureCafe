using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Business;
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
		IValidator<UserForRegisterDto> _validator;
		public UserService(IUserDal userDal, IMapper mapper, IValidator<UserForRegisterDto> validator)
		{
			_userDal = userDal;
			_mapper = mapper;
			_validator = validator;
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
		public IDataResult<TDto> Add<TDto>(TDto dto)
		{
			try
			{
				var entity = _mapper.Map<TDto, User>(dto);

				//business result
				IResult result = BusinessRules.Run();

				if (result != null)
				{
					return new ErrorDataResult<TDto>(dto, result.Message);
				}
				_userDal.Add(entity);
				return new SuccessDataResult<TDto>(dto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
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

		public IResult Update<TDto>(TDto dto)
		{
			try
			{
				//business result
				IResult result = BusinessRules.Run();
				var entity = _mapper.Map<TDto, User>(dto);

				if (result != null)
				{
					return new ErrorDataResult<TDto>(dto, result.Message);
				}
				_userDal.Update(entity);
				return new SuccessDataResult<TDto>(dto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
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
		public async Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id)
		{
			try
			{
				var userClass = await _userDal.FindByIdAsync(id);
				var userClassDto = _mapper.Map<User, TDto>(userClass);
				return new SuccessDataResult<TDto>(userClassDto);
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
		public async Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto)
		{
			try
			{
				var entity = _mapper.Map<TDto, User>(dto);

				//business result
				IResult result = BusinessRules.Run();

				if (result != null)
				{
					return new ErrorDataResult<TDto>(dto, result.Message);
				}
				await _userDal.AddAsync(entity);
				return new SuccessDataResult<TDto>(dto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
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
