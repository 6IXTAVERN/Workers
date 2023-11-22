using Workers.DataLayer.Interfaces;
using Workers.Domain.Models;

namespace Workers.DataLayer.Repositories;

public class ResumeRepository : IBaseRepository<Resume>
{
    private readonly ApplicationDbContext _db;

    public ResumeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Create(Resume entity)
    {
        await _db.Resumes.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(Resume entity)
    {
        _db.Resumes.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Resume> Update(Resume entity)
    {
        _db.Resumes.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public IQueryable<Resume> GetAll()
    {
        return _db.Resumes;
    }
}