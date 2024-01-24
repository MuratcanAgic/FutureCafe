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
  public class StockService : IStockService
  {
    IStockDal _stockDal;
    IValidator<Stock> _validator;
    IMapper _mapper;
    IProductDal _productDal;
    public StockService(IStockDal stockDal, IValidator<Stock> validator, IMapper mapper, IProductDal productDal)
    {
      _stockDal = stockDal;
      _validator = validator;
      _mapper = mapper;
      _productDal = productDal;
    }

    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Stock>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _stockDal.Add(entity);
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
        var entity = _mapper.Map<TDto, Stock>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        await _stockDal.AddAsync(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Stock, bool>> filter)
    {
      try
      {
        var exist = _stockDal.Any(filter);

        if (exist == true) return new SuccessResult(Messages.DataExist);

        return new ErrorResult(Messages.DataNotExist);
      }
      catch (Exception e)
      {
        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<int> CountWhere(Expression<Func<Stock, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Stock, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Stock>(dto);
        _stockDal.Delete(entity);
        return new SuccessDataResult<Stock>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Stock>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _stockDal.DeleteById(id);
        return new SuccessDataResult<Stock>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Stock>(e.Message);
      }
    }

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      try
      {
        return new SuccessDataResult<TDto>(_mapper.Map<Stock, TDto>(_stockDal.FindById(id)));
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
        var stock = await _stockDal.FindByIdAsync(id);
        var stockDto = _mapper.Map<Stock, TDto>(stock);
        return new SuccessDataResult<TDto>(stockDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<TDto> Get<TDto>(Expression<Func<Stock, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Stock, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Stock, bool>> filter = null, Func<IQueryable<Stock>, IOrderedQueryable<Stock>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var stockList = _stockDal.GetList(filter, orderBy, includeProperties);
        var dto = stockList.Select(e => _mapper.Map<Stock, TDto>(e)).ToList();
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
    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Stock, bool>> filter = null, Func<IQueryable<Stock>, IOrderedQueryable<Stock>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var stockList = await _stockDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = stockList.Select(e => _mapper.Map<Stock, TDto>(e)).ToList();
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

    public async Task<IDataResult<ProductTradeDto>> RemoveFromStockAsync(IEnumerable<ProductTradeDto> tradeProductDtos)
    {
      try
      {
        foreach (var productDto in tradeProductDtos)
        {
          var product = await _productDal.GetAsync(x => x.ProductBarcodNo == productDto.ProductBarcodNo);
          var productStocks = await GetListAsync<Stock>(x => x.ProductId == product.Id, null, "Product");
          var currentProductCount = productStocks.Data.Count() > 0 ? productStocks.Data.OrderByDescending(s => s.CreatedDate).FirstOrDefault().ProductCount : 0;
          var newProductCount = currentProductCount - productDto.ProductCount;

          var newStock = new Stock
          {
            ProductCount = newProductCount < 0 ? 0 : newProductCount,
            Product = product
          };
          if (currentProductCount > 0)
            await AddAsync(newStock);
        }

        return new SuccessDataResult<ProductTradeDto>(Messages.DataCreated);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<ProductTradeDto>(e.Message.ToString());
      }
    }

    public IResult Save()
    {
      try
      {
        _stockDal.Save();
        return new SuccessDataResult<Stock>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Stock>(e.Message);
      }
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _stockDal.SaveAsync();
        return new SuccessDataResult<Stock>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Stock>(e.Message);
      }
    }

    public IResult Update<TDto>(TDto dto)
    {
      try
      {
        //business result
        IResult result = BusinessRules.Run();
        var entity = _mapper.Map<TDto, Stock>(dto);

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _stockDal.Update(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<ValidationResult> Validate<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<Stock>(dto);

        var validationResult = _validator.Validate(entity);

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
