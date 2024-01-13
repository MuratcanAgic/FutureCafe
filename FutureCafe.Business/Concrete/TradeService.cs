using AutoMapper;
using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Results;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;
using System.Linq.Expressions;

namespace FutureCafe.Business.Concrete
{
  public class TradeService : ITradeService
  {
    ITradeDal _tradeDal;
    IStudentDal _studentDal;
    IProductDal _productDal;
    IMapper _mapper;
    public TradeService(ITradeDal tradeDal, IStudentDal studentDal, IProductDal productDal, IMapper mapper)
    {
      _tradeDal = tradeDal;
      _studentDal = studentDal;
      _productDal = productDal;
      _mapper = mapper;
    }

    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      throw new NotImplementedException();
    }

    public async Task<IDataResult<Trade>> AddAsync(string studentCardNumber, IEnumerable<ProductTradeDto> products)
    {
      try
      {
        var trade = new Trade();
        trade.TradeProduct = new List<TradeProduct>();
        trade.Student = await _studentDal.GetAsync(x => x.CardNumber == studentCardNumber);
        foreach (var item in products)
        {
          var product = await _productDal.GetAsync(x => x.ProductBarcodNo == item.ProductBarcodNo, "ProductPrice.Price");
          trade.TradeProduct.Add(new TradeProduct { ProductCount = item.ProductCount, Product = product, SalePriceSnap = product.ProductPrice.Select(x => x.Price).Where(x => x.SalePrice.HasValue).LastOrDefault().SalePrice });
        }
        await _tradeDal.AddAsync(trade);

        return new SuccessDataResult<Trade>(trade);

      }
      catch (Exception e)
      {
        return new ErrorDataResult<Trade>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Trade, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IDataResult<int> CountWhere(Expression<Func<Trade, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Trade, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete<TDto>(TDto dto)
    {
      throw new NotImplementedException();
    }

    public IResult DeleteById(int id)
    {
      throw new NotImplementedException();
    }

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id) where TDto : class
    {
      throw new NotImplementedException();
    }

    public IDataResult<TDto> Get<TDto>(Expression<Func<Trade, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Trade, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Trade, bool>> filter = null, Func<IQueryable<Trade>, IOrderedQueryable<Trade>> orderBy = null, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Trade, bool>> filter = null, Func<IQueryable<Trade>, IOrderedQueryable<Trade>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var productClassList = await _tradeDal.GetListAsync(filter, orderBy, includeProperties);

        var dto = productClassList.Select(e => _mapper.Map<Trade, TDto>(e)).ToList();
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
      throw new NotImplementedException();
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _tradeDal.SaveAsync();
        return new SuccessDataResult<Trade>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Trade>(e.Message);
      }
    }

    public IResult Update(Trade trade)
    {
      throw new NotImplementedException();
    }
  }
}
