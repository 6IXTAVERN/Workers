using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Workers.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Workers.Domain.Enum;
using Workers.Domain.ViewModels.Resume;
using Workers.Services.Interfaces;

namespace Workers.Controllers;

public class ResumeController : Controller
{
    private readonly IResumeService _resumeService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResumeController(IResumeService resumeService, IHttpContextAccessor httpContextAccessor)
    {
        _resumeService = resumeService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        TempData["UserId"] = userId; // сохраняем в TempData
        
        if (_resumeService.GetResumeByUserId(userId).Data != null)
        {
            return RedirectToAction("Edit");
        }
        
        var resumeModel = new CreateResumeViewModel
        {
            Faculties = Enum.GetValues(typeof(Faculty)).Cast<Faculty>().ToList()
        };
        
        return View(resumeModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateResumeViewModel model)
    {
        ModelState.Remove("UserId");
        var userId = TempData["UserId"]?.ToString();
        // если UserId есть в TempData, присваиваем его модели
        if (!string.IsNullOrEmpty(userId))
        {
            model.UserId = userId;
        }
        
        if (ModelState.IsValid)
        {
            var response = await _resumeService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                TempData["message"] = "Резюме успешно создано и сохранено";
                return RedirectToAction("Index", "Home");
            }
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
    
    [HttpGet]
    public IActionResult Edit(long id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var resume = _resumeService.GetResumeByUserId(userId).Data;
        
        var resumeModel = new CreateResumeViewModel()
        {
            /*
            FirstName = resume.FirstName,
            LastName = resume.LastName,
            MiddleName = resume.MiddleName,
            SelectedFaculty = resume.Faculty,
            */
            Faculties = Enum.GetValues(typeof(Faculty)).Cast<Faculty>().ToList()
        };

        return View(resumeModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(CreateResumeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        var response = await _resumeService.Create(model);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            TempData["message"] = "Резюме успешно создано и сохранено";
            return RedirectToAction("Index", "Home");
        }
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _resumeService.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("GetResumes");
        }

        return View("Error", $"{response.Description}");
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetResumes()
    {
        var response = _resumeService.GetResumes();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }
}