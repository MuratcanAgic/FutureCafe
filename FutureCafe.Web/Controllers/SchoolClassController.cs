﻿using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class SchoolClassController : Controller
  {
    ISchoolClassService _schoolClassService;

    public SchoolClassController(ISchoolClassService schoolClassService)
    {
      _schoolClassService = schoolClassService;
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
      var list = await _schoolClassService.GetListAsync<SchoolClassViewDto>();
      /*
            if (list.Data == null)
              return View("Error", new ErrorViewModel { ErrorMessage = list.Message });
      */
      return View(list.Data);
    }

    //CREATE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(SchoolClassCreateEditDto schoolClassDto)
    {
      if (schoolClassDto == null) { RedirectToAction("Index"); }

      //validate
      var validationResult = _schoolClassService.Validate(schoolClassDto);

      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        return View(schoolClassDto);
      }

      await _schoolClassService.AddAsync(schoolClassDto);
      await _schoolClassService.SaveAsync();

      return RedirectToAction("Index");
    }
    //EDIT
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync<SchoolClassCreateEditDto>(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Edit(SchoolClassCreateEditDto schoolClassDto)
    {
      if (schoolClassDto == null) { return RedirectToAction("Index"); }

      //var schoolClass = _schoolClassService.MapDtoToEntity<SchoolClassCreateEditDto, SchoolClass>(schoolClassDto);

      //validate
      var validationResult = _schoolClassService.Validate(schoolClassDto);
      if (validationResult.Data.IsValid == false)
      {
        validationResult.Data.AddToModelState(this.ModelState);
        return View(schoolClassDto);
      }

      _schoolClassService.Update(schoolClassDto);
      await _schoolClassService.SaveAsync();

      return RedirectToAction("Index");
    }
    //DETAIL
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync<SchoolClassViewDto>(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }

    //DELETE
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync<SchoolClassViewDto>(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      _schoolClassService.DeleteById(id);

      var saveResult = await _schoolClassService.SaveAsync();

      if (saveResult.Success == false)
      {
        return View("Error", new ErrorViewModel { ErrorMessage = saveResult.Message });
      }

      return RedirectToAction("Index");
    }
  }
}
