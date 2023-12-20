using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Workers.DataLayer.Interfaces;
using Workers.Domain.Enum;
using Workers.Domain.Interfaces;
using Workers.Domain.Models;
using Workers.Domain.Response;
using Workers.Domain.ViewModels.Resume;
using Workers.Services.Interfaces;
using Workers.Services.Implementations;
using Workers.DataLayer.Repositories;

namespace Workers.Services.Implementations;

public class ResumeService : IResumeService
{
    private readonly IResumeRepository _resumeRepository;
    
    public ResumeService(IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;
    }

    public async Task<IBaseResponse<Resume>> Create(CreateResumeViewModel model)
    {
        try
        {
            var resume = new Resume
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                DateCreated = DateTime.Now,
                Faculty = model.SelectedFaculty,
                UserId = model.UserId
            };
     
            await _resumeRepository.Create(resume);

            return new BaseResponse<Resume>("Резюме создано", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Resume>(ex.Message, StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<bool>> Delete(long resumeId)
    {
        try
        {
            var resume = await _resumeRepository.GetResumeById(resumeId);

            if (resume == null)
            {
                return new BaseResponse<bool>("Резюме не найдено", StatusCode.OrderNotFound);
            }

            await _resumeRepository.Delete(resume);
            return new BaseResponse<bool>("Резюме удалено", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>(ex.Message, StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<Resume>> Edit(long resumeId, CreateResumeViewModel model)
    {
        try
        {
            var resume = await _resumeRepository.GetResumeById(resumeId);

            if (resume == null)
            {
                return new BaseResponse<Resume>("Резюме не найдено", StatusCode.OrderNotFound);
            }

            // TODO Заимплементить изменение полей Resume для апдейта 
            
            await _resumeRepository.Update(resume);
            return new BaseResponse<Resume>("Резюме отредактировано", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Resume>($"[Resume.Edit] : {ex.Message}", StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<List<Resume>>> GetResumes()
    {
        try
        {
            var resumes = _resumeRepository.GetAll().ToList();
            
            return resumes.Count == 0 ? 
                new BaseResponse<List<Resume>>("Найдено 0 элементов", StatusCode.Ok, resumes) : 
                new BaseResponse<List<Resume>>("Получены существующие резюме", StatusCode.Ok, resumes);
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Resume>>($"[Resume.GetResumes] : {ex.Message}", StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Resume>> GetResumeByUserId(string userId)
    {
        try
        {
            var resume = await _resumeRepository.GetResumeByUserId(userId);
            return new BaseResponse<Resume>("Получено резюме пользователя", StatusCode.Ok, resume);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Resume>($"[GetResumeByUserId] : {ex.Message}", StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<Resume>> GetResumeById(long resumeId)
    {
        try
        {
            var resume = await _resumeRepository.GetResumeById(resumeId);
            return new BaseResponse<Resume>("Получено резюме пользователя", StatusCode.Ok, resume);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Resume>($"[GetResumeByUserId] : {ex.Message}", StatusCode.InternalServerError);
        }
    }
}