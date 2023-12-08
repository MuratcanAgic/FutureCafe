using AutoMapper;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Core.Utilities.Business;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
	public class OperationClaimService : IOperationClaimService
	{
		IOperationClaimDal _operationClaimDal;
		//IValidator<OperationClaim> _validator;
		IMapper _mapper;

		public OperationClaimService(IOperationClaimDal operationClaimDal, IMapper mapper)
		{
			_operationClaimDal = operationClaimDal;
			//_validator = validator;
			_mapper = mapper;
		}

		public IDataResult<TDto> Add<TDto>(TDto dto)
		{
			try
			{
				var entity = _mapper.Map<TDto, OperationClaim>(dto);

				//business result
				IResult result = BusinessRules.Run();

				if (result != null)
				{
					return new ErrorDataResult<TDto>(dto, result.Message);
				}
				_operationClaimDal.Add(entity);
				return new SuccessDataResult<TDto>(dto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
			}
		}
		public async Task<IDataResult<TDto>> AddAsync<TDto>(TDto dto)
		{
			try
			{
				var entity = _mapper.Map<TDto, OperationClaim>(dto);

				//business result
				IResult result = BusinessRules.Run();

				if (result != null)
				{
					return new ErrorDataResult<TDto>(dto, result.Message);
				}
				await _operationClaimDal.AddAsync(entity);
				return new SuccessDataResult<TDto>(dto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
			}
		}

		public IResult Any(Expression<Func<OperationClaim, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public IDataResult<int> CountWhere(Expression<Func<OperationClaim, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public Task<IDataResult<int>> CountWhereAsync(Expression<Func<OperationClaim, bool>> filter)
		{
			throw new NotImplementedException();
		}

		public IResult Delete<TDto>(TDto dto)
		{
			try
			{
				var entity = _mapper.Map<TDto, OperationClaim>(dto);
				_operationClaimDal.Delete(entity);
				return new SuccessDataResult<OperationClaim>(Messages.DataDeleted);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<OperationClaim>(e.Message);
			}
		}

		public IResult DeleteById(int id)
		{
			try
			{
				_operationClaimDal.DeleteById(id);
				return new SuccessDataResult<OperationClaim>(Messages.DataDeleted);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<OperationClaim>(e.Message);
			}
		}

		public IDataResult<TDto> FindById<TDto>(int id)
		{
			try
			{
				return new SuccessDataResult<TDto>(_mapper.Map<OperationClaim, TDto>(_operationClaimDal.FindById(id)));
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
			}
		}

		public async Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id)
		{
			try
			{
				var schoolClass = await _operationClaimDal.FindByIdAsync(id);
				var schoolClassDto = _mapper.Map<OperationClaim, TDto>(schoolClass);
				return new SuccessDataResult<TDto>(schoolClassDto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
			}
		}

		public IDataResult<TDto> Get<TDto>(Expression<Func<OperationClaim, bool>> filter, string includeProperties = "")
		{
			throw new NotImplementedException();
		}

		public Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<OperationClaim, bool>> filter, string includeProperties = "")
		{
			throw new NotImplementedException();
		}

		public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<OperationClaim, bool>> filter = null, Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>> orderBy = null, string includeProperties = "")
		{
			try
			{
				var schoolClassList = _operationClaimDal.GetList(filter, orderBy, includeProperties);
				var dto = schoolClassList.Select(e => _mapper.Map<OperationClaim, TDto>(e)).ToList();
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
		public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<OperationClaim, bool>> filter = null, Func<IQueryable<OperationClaim>, IOrderedQueryable<OperationClaim>> orderBy = null, string includeProperties = "")
		{
			try
			{
				var schoolClassList = await _operationClaimDal.GetListAsync(filter, orderBy, includeProperties);
				var dto = schoolClassList.Select(e => _mapper.Map<OperationClaim, TDto>(e)).ToList();
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



		public IResult Save()
		{
			try
			{
				_operationClaimDal.Save();
				return new SuccessDataResult<OperationClaim>(Messages.DataSaved);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<OperationClaim>(e.Message);
			}
		}

		public async Task<IResult> SaveAsync()
		{
			try
			{
				await _operationClaimDal.SaveAsync();
				return new SuccessDataResult<OperationClaim>(Messages.DataSaved);
			}
			catch (DbUpdateException ex) when (IsForeignKeyViolation(ex))
			{
				return new ErrorDataResult<SchoolClass>("Bu kategoriyle ilişkili ürünler olduğundan, kategori silinemez. Öncelikle ürünler silinmeli.");
			}
			catch (Exception e)
			{
				return new ErrorDataResult<OperationClaim>(e.Message);
			}
		}
		private bool IsForeignKeyViolation(DbUpdateException ex)
		{
			return ex?.InnerException is SqlException sqlException &&
						 (sqlException.Number == 547 || sqlException.Number == 2601);
		}
		public IResult Update<TDto>(TDto dto)
		{
			try
			{
				//business result
				IResult result = BusinessRules.Run();
				var entity = _mapper.Map<TDto, OperationClaim>(dto);

				if (result != null)
				{
					return new ErrorDataResult<TDto>(dto, result.Message);
				}
				_operationClaimDal.Update(entity);
				return new SuccessDataResult<TDto>(dto);
			}
			catch (Exception e)
			{
				return new ErrorDataResult<TDto>(e.Message);
			}
		}

		//public IDataResult<ValidationResult> Validate<TDto>(TDto dto)
		//{
		//	try
		//	{
		//		var entity = _mapper.Map<OperationClaim>(dto);

		//		var validationResult = _validator.Validate(entity);

		//		if (validationResult == null)
		//			return new ErrorDataResult<ValidationResult>(Messages.ValidationResultNull);

		//		return new SuccessDataResult<ValidationResult>(validationResult);

		//	}
		//	catch (Exception e)
		//	{
		//		return new ErrorDataResult<ValidationResult>(e.Message);
		//	}
		//}
	}
}
