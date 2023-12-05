using Workers.Domain.Models;

namespace Workers.DataLayer.Interfaces;

public interface IResumeRepository : IBaseRepository<Resume>
{
    public Task<Resume> GetResumeById(long resumeId);
    public Task<Resume> GetResumeByUserId(string userId);
}