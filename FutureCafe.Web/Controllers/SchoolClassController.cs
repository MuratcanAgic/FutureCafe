﻿using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Core.Utilities.Extensions;
using FutureCafe.Entities.Concrete;
using FutureCafe.Web.Models;
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

    public async Task<IActionResult> Index()
    {
      var list = await _schoolClassService.GetListAsync();

      if (list.Data == null)
        return View("Error", new ErrorViewModel { ErrorMessage = list.Message });

      return View(list.Data);
    }
    //CREATE
    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(SchoolClassCreateEditDto schoolClassDto)
    {
      if (schoolClassDto == null) { RedirectToAction("Index"); }

      var schoolClass = _schoolClassService.MapDtoToEntity<SchoolClassCreateEditDto, SchoolClass>(schoolClassDto);
      //validate
      var validationResult = await _schoolClassService.ValidateAsync(schoolClass.Data);
      if (validationResult.IsValid == false)
      {
        validationResult.AddToModelState(this.ModelState);
        return View(schoolClassDto);
      }
      await _schoolClassService.AddAsync(schoolClass.Data);
      await _schoolClassService.SaveAsync();

      return RedirectToAction("Index");
    }
    //EDIT
    public async Task<IActionResult> Edit(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      var schoolClassDto = _schoolClassService.MapEntityToDto<SchoolClass, SchoolClassCreateEditDto>(schoolClass.Data);

      return View(schoolClassDto.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(SchoolClassCreateEditDto schoolClassDto)
    {
      if (schoolClassDto == null) { return RedirectToAction("Index"); }

      var schoolClass = _schoolClassService.MapDtoToEntity<SchoolClassCreateEditDto, SchoolClass>(schoolClassDto);

      //validate
      var validationResult = await _schoolClassService.ValidateAsync(schoolClass.Data);
      if (validationResult.IsValid == false)
      {
        validationResult.AddToModelState(this.ModelState);
        return View(schoolClassDto);
      }

      _schoolClassService.Update(schoolClass.Data);
      await _schoolClassService.SaveAsync();

      return RedirectToAction("Index");
    }
    //DETAIL
    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }

    //DELETE

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      var schoolClass = await _schoolClassService.FindByIdAsync(id);

      if (schoolClass == null)
      { return RedirectToAction("Index"); }

      return View(schoolClass.Data);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
      if (id == 0) { return RedirectToAction("Index"); }

      _schoolClassService.DeleteById(id);
      await _schoolClassService.SaveAsync();

      return RedirectToAction("Index");
    }
  }
}
