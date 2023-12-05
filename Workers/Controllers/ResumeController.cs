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
    public async Task<IActionResult> Manage(long id)
    {
        var activeUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        TempData["userId"] = activeUserId;
        var response = await _resumeService.GetResumeByUserId(activeUserId!);
        var resume = response.Data;
        var resumeModel = new CreateResumeViewModel();
        
        if (resume != null)
        {
            TempData["resumeId"] = resume.Id.ToString();
            resumeModel = new CreateResumeViewModel
            {
                Id = resume.Id,
                FirstName = resume.FirstName,
                LastName = resume.LastName,
                MiddleName = resume.MiddleName,
                SelectedFaculty = resume.Faculty,
                Faculties = Enum.GetValues(typeof(Faculty)).Cast<Faculty>().ToList()
            };
        }
        resumeModel.Faculties = Enum.GetValues(typeof(Faculty)).Cast<Faculty>().ToList();
            
        return View(resumeModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Manage(CreateResumeViewModel model)
    {
        ModelState.Remove("Id");
        ModelState.Remove("UserId");
        model.Id = long.Parse(TempData["resumeId"]!.ToString()!);
        model.UserId = TempData["userId"]?.ToString()!;
        if (ModelState.IsValid)
        {
            if (model.Id == 0)
            {
                var response = await _resumeService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    TempData["message"] = "Резюме успешно создано и сохранено";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var response = await _resumeService.Edit(model.Id, model);
                if (response.StatusCode == Domain.Enum.StatusCode.Ok)
                {
                    TempData["message"] = "Резюме успешно изменено";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
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