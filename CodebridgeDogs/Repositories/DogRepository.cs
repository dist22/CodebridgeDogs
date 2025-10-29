using CodebridgeDogs.Data.Context;
using CodebridgeDogs.Interfaces;
using CodebridgeDogs.Interfaces.IRepositories;
using CodebridgeDogs.Models;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeDogs.Repositories;

public class DogRepository(DataContextEf entity) : IDogRepository
{
    private readonly DbSet<Dog> _dbSet = entity.Set<Dog>();

    public IQueryable<Dog> GetQueryable()
        => _dbSet
            .AsNoTracking()
            .AsQueryable();

    public async Task<bool> ExistAsync(string dogName) 
        =>  await _dbSet.AnyAsync(d => d.Name == dogName);

    public async Task AddAsync(Dog dog) 
        => await _dbSet.AddAsync(dog);

    public async Task<bool> SaveChanges() 
        => await entity.SaveChangesAsync() > 0;
}