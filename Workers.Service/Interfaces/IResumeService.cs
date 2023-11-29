using Workers.Domain.Interfaces;
using Workers.Domain.Models;
using Workers.Domain.ViewModels.Resume;

namespace Workers.Services.Interfaces;

public interface IResumeService
{
    Task<IBaseResponse<Resume>> Create(CreateResumeViewModel model);

    Task<IBaseResponse<bool>> Delete(long id);
    
    //public IBaseResponse<Resume> GetResumeByUserId();

    public IBaseResponse<List<Resume>> GetResumes();
    
    public IBaseResponse<Resume> GetActiveUserResume();
}