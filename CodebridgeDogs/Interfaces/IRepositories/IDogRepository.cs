using CodebridgeDogs.Models;

namespace CodebridgeDogs.Interfaces.IRepositories;

public interface IDogRepository 
{
    public IQueryable<Dog> GetQueryable();
    public Task<bool> ExistAsync(string dogName);
    public Task AddAsync(Dog dog);
    public Task<bool> SaveChanges();
    
}