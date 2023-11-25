using Workers.Domain.Models;

namespace Workers.DataLayer.Interfaces;

public interface IResumeRepository : IBaseRepository<Resume>
{
    public Resume GetResumeByUserId(string userId);
}