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
  public class StudentService : IStudentService
  {
    IStudentDal _studentDal;
    IValidator<Student> _validator;
    IMapper _mapper;
    ICategoryDal _categoryDal;
    IProductDal _productDal;
    public StudentService(IStudentDal studentDal, IValidator<Student> validator, IMapper mapper, ICategoryDal categoryDal, IProductDal productDal)
    {
      _studentDal = studentDal;
      _validator = validator;
      _mapper = mapper;
      _categoryDal = categoryDal;
      _productDal = productDal;
    }

    public async Task<IDataResult<List<ProductBanDto>>> GetProductsByCagegoryToBanAsync(int studentId)
    {
      try
      {
        List<ProductBanDto> dtos = new List<ProductBanDto>();
        var categories = await _categoryDal.GetListAsync();
        var products = await _productDal.GetListAsync(null, null, "ProductCategory");
        var studentList = _studentDal.GetList(null, null, "StudentCategory,StudentProduct.Product.ProductCategory");
        Student student = studentList.FirstOrDefault(x => x.Id == studentId);

        if (student == null || products == null || categories == null)
          return new ErrorDataResult<List<ProductBanDto>>(Messages.DataNotFound);


        foreach (var category in categories)
        {
          var productsByCategory = products.Where(p => p.ProductCategory.Any(x => x.CategoryId == category.Id)).ToList();
          var selectIds = new List<int>();

          if (student.StudentCategory.Any(x => x.CategoryId == category.Id)) selectIds.Add(-1);

          foreach (var studentProduct in student.StudentProduct)
          {
            if (studentProduct.Product.ProductCategory.Any(x => x.CategoryId == category.Id))
            {
              selectIds.Add(studentProduct.ProductId);
            }
          }

          dtos.Add(new ProductBanDto { CategoryName = category.Name, ProductList = productsByCategory, SelectedProductIds = selectIds, CategoryId = category.Id });
        }

        return new SuccessDataResult<List<ProductBanDto>>(dtos);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<List<ProductBanDto>>(e.Message);

      }
    }

    public async Task<IDataResult<string>> CheckBannedProducts(IEnumerable<ProductTradeDto> productsToCheck, string studentCardNumber)
    {
      try
      {
        var student = await GetAsync<Student>(x => x.CardNumber == studentCardNumber, "StudentCategory.Category,StudentProduct.Product");
        var productBarcodes = productsToCheck.Select(p => p.ProductBarcodNo).Distinct().ToList();
        var products = await _productDal.GetListAsync(x => productBarcodes.Contains(x.ProductBarcodNo), null, "ProductCategory.Category");

        if (student == null || products == null) return new ErrorDataResult<string>(Messages.DataNotFound);

        List<string> result = new List<string>();

        foreach (var product in products)
        {
          foreach (var studentProduct in student.Data.StudentProduct)
          {

            if (studentProduct.ProductId == product.Id)
            {
              result.Add($"{product.Name} ürünü {student.Data.NameSurname} için yasaklıdır!");
            }
          }

          if (product.ProductCategory.Any(x => student.Data.StudentCategory.Any(y => y.CategoryId == x.CategoryId)))
          {
            result.Add($"{product.Name} ürünü {student.Data.NameSurname} için yasaklıdır!");
          }
        }

        string concatenatedString = string.Join(Environment.NewLine, result);

        return new SuccessDataResult<string>(concatenatedString);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<string>(e.Message);
      }
    }

    public IResult BanUpdate(int studentId, List<ProductBanDto> banDtos)
    {
      try
      {
        var studentList = _studentDal.GetList(null, null, "StudentCategory,StudentProduct");
        Student student = studentList.FirstOrDefault(x => x.Id == studentId);

        if (student == null)
          return new ErrorResult(Messages.DataNotFound);


        var studentCategoriesBanList = new List<StudentCategory>();
        var studentProductsBanList = new List<StudentProduct>();


        foreach (var dto in banDtos)
        {
          if (dto.SelectedProductIds != null)
          {
            if (dto.SelectedProductIds.Contains(-1))
            {
              studentCategoriesBanList.Add(new StudentCategory { CategoryId = dto.CategoryId });
            }
            else
            {
              foreach (var productId in dto.SelectedProductIds)
              {
                if (studentProductsBanList.Any(x => x.ProductId == productId) == false)
                  studentProductsBanList.Add(new StudentProduct { ProductId = productId });
              }
            }
          }
        }

        student.StudentProduct = studentProductsBanList;
        student.StudentCategory = studentCategoriesBanList;


        return new SuccessResult("Ban kaydedildi");
      }
      catch (Exception e)
      {

        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<TDto> Add<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Student>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _studentDal.Add(entity);
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
        var entity = _mapper.Map<TDto, Student>(dto);

        //business result
        IResult result = BusinessRules.Run();

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        await _studentDal.AddAsync(entity);
        return new SuccessDataResult<TDto>(dto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IResult Any(Expression<Func<Student, bool>> filter)
    {
      try
      {
        var exist = _studentDal.Any(filter);

        if (exist == true) return new SuccessResult(Messages.DataExist);

        return new ErrorResult(Messages.DataNotExist);
      }
      catch (Exception e)
      {
        return new ErrorResult(e.Message);
      }
    }

    public IDataResult<int> CountWhere(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public Task<IDataResult<int>> CountWhereAsync(Expression<Func<Student, bool>> filter)
    {
      throw new NotImplementedException();
    }

    public IResult Delete<TDto>(TDto dto)
    {
      try
      {
        var entity = _mapper.Map<TDto, Student>(dto);
        _studentDal.Delete(entity);
        return new SuccessDataResult<Student>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IResult DeleteById(int id)
    {
      try
      {
        _studentDal.DeleteById(id);
        return new SuccessDataResult<Student>(Messages.DataDeleted);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message);
      }
    }

    public IDataResult<TDto> FindById<TDto>(int id)
    {
      try
      {
        return new SuccessDataResult<TDto>(_mapper.Map<Student, TDto>(_studentDal.FindById(id)));
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
        var schoolClass = await _studentDal.FindByIdAsync(id);
        var schoolClassDto = _mapper.Map<Student, TDto>(schoolClass);
        return new SuccessDataResult<TDto>(schoolClassDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message);
      }
    }

    public IDataResult<TDto> Get<TDto>(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      throw new NotImplementedException();
    }

    public async Task<IDataResult<TDto>> GetAsync<TDto>(Expression<Func<Student, bool>> filter, string includeProperties = "")
    {
      try
      {
        var student = await _studentDal.GetAsync(filter, includeProperties);
        var studentDto = _mapper.Map<Student, TDto>(student);
        if (studentDto == null)
        {
          return new ErrorDataResult<TDto>(Messages.DataNotFound);
        }
        return new SuccessDataResult<TDto>(studentDto);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<TDto>(e.Message.ToString());
      }
    }

    public IDataResult<IEnumerable<TDto>> GetList<TDto>(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = _studentDal.GetList(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<Student, TDto>(e)).ToList();
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

    public IDataResult<decimal?> GetLastCreditAmount(Student student)
    {
      try
      {
        var salePrice = student.StudentCredit.Select(x => x.Credit).LastOrDefault().Amount;
        if (salePrice != null)
          return new SuccessDataResult<decimal?>(salePrice);

        return new ErrorDataResult<decimal?>("Öğrenci kartında kredi bulunmuyor");
      }
      catch (Exception e)
      {
        return new ErrorDataResult<decimal?>(e.Message);
      }
    }
    public async Task<IDataResult<IEnumerable<TDto>>> GetListAsync<TDto>(Expression<Func<Student, bool>> filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null, string includeProperties = "")
    {
      try
      {
        var schoolClassList = await _studentDal.GetListAsync(filter, orderBy, includeProperties);
        var dto = schoolClassList.Select(e => _mapper.Map<Student, TDto>(e)).ToList();
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

    public IDataResult<Student> LoadMoneyToStudent(Student student, decimal loadAmount)
    {
      try
      {
        var lastAmount = student.StudentCredit == null || student.StudentCredit.LastOrDefault() == null ? 0 : student.StudentCredit.LastOrDefault().Credit.Amount;
        var newCreditAmount = new Credit { Amount = loadAmount + lastAmount };

        student.StudentCredit.Add(new StudentCredit { Credit = newCreditAmount, Student = student });

        return new SuccessDataResult<Student>(student);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message.ToString());
      }

    }

    public IDataResult<Student> WithDrawMoneyFromStudent(Student student, decimal withdrawAmount)
    {
      try
      {
        var lastAmount = student.StudentCredit == null || student.StudentCredit.LastOrDefault() == null ? 0 : student.StudentCredit.LastOrDefault().Credit.Amount;
        var newCreditAmount = new Credit { Amount = lastAmount - withdrawAmount };

        student.StudentCredit.Add(new StudentCredit { Credit = newCreditAmount, Student = student });

        return new SuccessDataResult<Student>(student);
      }
      catch (Exception e)
      {
        return new ErrorDataResult<Student>(e.Message.ToString());
      }

    }

    public IResult Save()
    {
      try
      {
        _studentDal.Save();
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
        await _studentDal.SaveAsync();
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
        var entity = _mapper.Map<TDto, Student>(dto);

        if (result != null)
        {
          return new ErrorDataResult<TDto>(dto, result.Message);
        }
        _studentDal.Update(entity);
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
        var entity = _mapper.Map<Student>(dto);

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
