using Workers.DataLayer.Interfaces;
using Workers.Domain.Models;

namespace Workers.DataLayer.Repositories;

public class UserRepository : IBaseRepository<User>
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(User entity)
    {
        _db.Users.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<User> Update(User entity)
    {
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    public IQueryable<User> GetAll()
    {
        return _db.Users;
    }
}