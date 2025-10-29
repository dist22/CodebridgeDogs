using CodebridgeDogs.Data.Configuration;
using CodebridgeDogs.Models;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeDogs.Data.Context;

public class DataContextEf(DbContextOptions<DataContextEf> options) : DbContext(options)
{
    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DogConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}