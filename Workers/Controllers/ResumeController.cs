using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Workers.Domain.Models;
using Microsoft.AspNetCore.Http;
using Workers.Domain.ViewModels.Resume;
using Workers.Services.Interfaces;

namespace Workers.Controllers;

public class ResumeController : Controller
{
    private readonly IResumeService _resumeService;
    private readonly UserManager<User> _userManager;

    public ResumeController(IResumeService resumeService, UserManager<User> userManager)
    {
        _resumeService = resumeService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Create(long id)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        var resumeModel = new CreateResumeViewModel()
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName
        };

        return View(resumeModel);
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