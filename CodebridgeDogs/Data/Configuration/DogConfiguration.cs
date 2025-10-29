using CodebridgeDogs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodebridgeDogs.Data.Configuration;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.ToTable("Dogs");

        builder.HasKey(d => d.Name);

        builder.Property(d => d.Name)
            .IsRequired();
        
        builder.HasIndex(d => d.Name)
            .IsUnique();
        
        builder.Property(d => d.Color)
            .IsRequired();
        
        builder.Property(d => d.TailLenght)
            .IsRequired();
        
        builder.Property(d => d.Weight)
            .IsRequired();

    }
}