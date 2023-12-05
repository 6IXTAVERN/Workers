using Workers.Domain.Models;

namespace Workers.DataLayer.Interfaces;

public interface IResumeRepository : IBaseRepository<Resume>
{
    public Resume GetResumeById(long resumeId);
    public Resume GetResumeByUserId(string userId);
}