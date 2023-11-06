using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace FutureCafe.Core.Utilities.Extensions
{
  public static class ValidationResultExtension
  {
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
      modelState.Clear();
      foreach (var error in result.Errors)
      {
        modelState.AddModelError(error.PropertyName, error.ErrorMessage);
      }
    }
  }
}
