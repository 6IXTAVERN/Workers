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

namespace Workers.Services.Implementations;

public class ResumeService : IResumeService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResumeRepository _resumeRepository;
    
    public ResumeService(IResumeRepository resumeRepository, IHttpContextAccessor httpContextAccessor)
    {
        _resumeRepository = resumeRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IBaseResponse<Resume>> Create(CreateResumeViewModel model)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            
            if (userId == null)
            {
                return new BaseResponse<Resume>()
                {
                    Description = "Пользователь не найден",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var resume = new Resume()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                DateCreated = DateTime.Now,
                UserId = userId
            };
     
            await _resumeRepository.Create(resume);

            return new BaseResponse<Resume>()
            {
                Description = "Резюме создано",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Resume>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(long id)
    {
        try
        {
            var resume = _resumeRepository.GetAll()
                .FirstOrDefault(x => x.Id == id);

            if (resume == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Резюме не найдено",
                    StatusCode = StatusCode.OrderNotFound
                };
            }

            await _resumeRepository.Delete(resume);
            return new BaseResponse<bool>()
            {
                Description = "Резюме удалено",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    
    public IBaseResponse<List<Resume>> GetResumes()
    {
        try
        {
            var resumes = _resumeRepository.GetAll().ToList();
            if (!resumes.Any())
            {
                return new BaseResponse<List<Resume>>
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok
                };
            }
                
            return new BaseResponse<List<Resume>>
            {
                Data = resumes,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Resume>>
            {
                Description = $"[GetResumes] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public IBaseResponse<Resume> GetActiveUserResume()
    {
        var userId = _httpContextAccessor.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        try
        {
            var resume = _resumeRepository.GetResumeByUserId(userId);
            if (resume == null)
            {
                return new BaseResponse<Resume>
                {
                    Description = "Резюме не найдено",
                    StatusCode = StatusCode.Ok
                };
            }
                
            return new BaseResponse<Resume>
            {
                Data = resume,
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Resume>
            {
                Description = $"[GetResumeByUserId] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}