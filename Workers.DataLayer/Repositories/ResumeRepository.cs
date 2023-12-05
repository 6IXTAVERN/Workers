using Microsoft.EntityFrameworkCore;
using Workers.DataLayer.Interfaces;
using Workers.Domain.Models;

namespace Workers.DataLayer.Repositories;

public class ResumeRepository : IResumeRepository
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
        return _db.Resumes.AsQueryable()!;
    }
    
    public async Task<Resume> GetResumeById(long resumeId)
    {
        return (await _db.Resumes.FirstOrDefaultAsync(r => r!.Id == resumeId))!;
    }
    
    public async Task<Resume> GetResumeByUserId(string userId)
    {
        return (await _db.Resumes.FirstOrDefaultAsync(r => r!.UserId == userId))!;
    }
}