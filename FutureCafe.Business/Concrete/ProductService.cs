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
  public class ProductService : IProductService
  {

    IProductDal _productDal;
    ICategoryDal _categoryDal;
    IValidator<ProductCreateEditDto> _validator;
    IMapper _mapper;

    public ProductService(IProductDal productDal, IValidator<ProductCreateEditDto> validator, IMapper mapper, ICategoryDal categoryDal)
    {
      _productDal = productDal;
      _validator = validator;
      _mapper = mapper;
      _categoryDal = categoryDal;
    }
    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Product>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _productDal.Add(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }
    public async Task<IDataResult<ProductCreateEditDto>> AddAsync(ProductCreateEditDto dto)
    {
      try
      {
        if (dto.SelectCategoryIds != null && dto.SelectCategoryIds.Any())
        {
          dto.ProductCategory = dto.SelectCategoryIds
              .Select(categoryId => new ProductCategory { CategoryId = categoryId })
              .ToList();
        }
        dto.ProductPrice = new List<ProductPrice>() { new ProductPrice { Price = new Price { BuyingPrice = dto.BuyingPrice, SalePrice = dto.SalePrice } } };

        var entity = _mapper.Map<ProductCreateEditDto, Product>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<ProductCreateEditDto>(dto, result.Message);
        }
        await _productDal.AddAsync(entity);
        return new SuccessDataResult<ProductCreateEditDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<ProductCreateEditDto>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Product, bool>> filter)
    {
      try
      {
        var exist = _productDal.Any(filter);

        if (exist == true) return new SuccessResult(Messages.DataExist);

        return new ErrorResult(Messages.DataNotExist);
      }
      catch (Exception e)
      {
        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<int> CountWhere(Expression<Func<Product, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Product, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Product>(dto);
        _productDal.Delete(entity);
        return new SuccessDataResult<Product>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Product>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _productDal.DeleteById(id);
        return new SuccessDataResult<Product>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Product>(e.Message);
      }
    }

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      try
      {
        return new SuccessDataResult<TDto>(_mapper.Map<Product, TDto>(_productDal.FindById(id)));
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public async Task<IDataResult<TDto>> FindByIdAsync<TDto>(int id) where TDto : class
    {
      try
      {
        var productList = await _productDal.GetListAsync(null, null, "ProductCategory.Category,ProductPrice.Price");
        var productClass = productList.Where(x => x.Id == id).FirstOrDefault();

        if (typeof(TDto) == typeof(ProductCreateEditDto))
        {
          var productClassDto = _mapper.Map<Product, ProductCreateEditDto>(productClass);
          productClassDto.SelectCategoryIds = productClassDto.ProductCategory.Select(x => x.CategoryId).ToList();

          var buyingPrice = productClassDto.ProductPrice.Select(x => x.Price).Where(x => x.BuyingPrice.HasValue).LastOrDefault();
          var salePrice = productClassDto.ProductPrice.Select(x => x.Price).Where(x => x.SalePrice.HasValue).LastOrDefault();

          productClassDto.BuyingPrice = buyingPrice != null ? buyingPrice.BuyingPrice : 0M;
          productClassDto.SalePrice = salePrice != null ? salePrice.SalePrice : 0M;

          return new SuccessDataResult<TDto>(productClassDto as TDto);
        }
        else if (typeof(TDto) == typeof(ProductDetailDto))
        {
          var productViewDto = _mapper.Map<Product, ProductDetailDto>(productClass);
          return new SuccessDataResult<TDto>(productViewDto as TDto);
        }

        else if (typeof(TDto) == typeof(Product))
        {
          return new SuccessDataResult<TDto>(productClass as TDto);
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
    public IDataResult<decimal?> GetLastSalePrice(Product product)
    {
      try
      {
        var salePrice = product.ProductPrice.Select(x => x.Price).Where(x => x.SalePrice.HasValue).LastOrDefault().SalePrice;
        if (salePrice != null)
          return new SuccessDataResult<decimal?>(salePrice);

        return new ErrorDataResult<decimal?>(product.Name + "ürününün satış fiyatı tanımlanmamış");
      }
      catch (Exception e)
      {
        return new ErrorDataResult<decimal?>(e.Message);
      }
    }
    public IDataResult<TDto> Get<TDto>(Expression<Func<Product, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public async Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Product, bool>> filter, string includeProperties = "")
    {
      try
      {
        var product = await _productDal.GetAsync(filter, includeProperties);
        var productDto = _mapper.Map<Product, TDto>(product);

        if (productDto == null)
        {
          return new ErrorDataResult<TDto>(Messages.DataNotFound);
        }
        return new SuccessDataResult<TDto>(productDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message.ToString());
      }
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var productClassList = _productDal.GetList(filter, orderBy, includeProperties);
        var dto = productClassList.Select(e => _mapper.Map<Product, TDto>(e)).ToList();
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
    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Product, bool>> filter = null, Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var productClassList = await _productDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = productClassList.Select(e => _mapper.Map<Product, TDto>(e)).ToList();
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
        _productDal.Save();
        return new SuccessDataResult<Product>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Product>(e.Message);
      }
    }

    public async Task<IResult> SaveAsync()
    {
      try
      {
        await _productDal.SaveAsync();
        return new SuccessDataResult<Product>(Messages.DataSaved);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Product>(e.Message);
      }
    }

    public IResult Update(ProductCreateEditDto dto)
    {
      try
      {
        var productList = _productDal.GetList(null, null, "ProductCategory.Category,ProductPrice.Price");
        var entity = productList.Where(x => x.Id == dto.Id).FirstOrDefault();

        if (entity != null)
        {
          entity.ProductCategory.Clear();

          if (dto.SelectCategoryIds != null && dto.SelectCategoryIds.Any())
          {
            foreach (var item in dto.SelectCategoryIds)
            {
              var category = _categoryDal.FindById(item);
              entity.ProductCategory.Add(new ProductCategory { Category = category, Product = entity });
            }
          }
          var newPrice = new Price { BuyingPrice = dto.BuyingPrice, SalePrice = dto.SalePrice };
          entity.ProductPrice.Add(new ProductPrice { Price = newPrice, Product = entity });
          entity.Name = dto.Name;
          entity.Description = dto.Description;
          entity.ProductBarcodNo = dto.ProductBarcodNo;
          entity.ImageUrl = dto.ImageUrl;

          //business result
          IResult result = BusinessRules.Run();
          if (result != null)
          {
            return new ErrorDataResult<ProductCreateEditDto>(dto, result.Message);
          }
          _productDal.Update(entity);
          return new SuccessDataResult<ProductCreateEditDto>(dto);
        }

        return new ErrorDataResult<ProductCreateEditDto>(Messages.ListEmpty);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<ProductCreateEditDto>(e.Message);
      }
    }

    public IDataResult<ValidationResult> Validate(ProductCreateEditDto dto)
    {
      try
      {

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
