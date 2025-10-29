using CodebridgeDogs.Dto_s;
using CodebridgeDogs.Enums;
using CodebridgeDogs.Exceptions;
using CodebridgeDogs.Interfaces;
using CodebridgeDogs.Interfaces.IRepositories;
using CodebridgeDogs.Interfaces.IServices;
using CodebridgeDogs.Models;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeDogs.Services;

public class DogService(IDogRepository dogRepository) : IDogService
{
    public async Task<IEnumerable<Dog>> GetDogsAsync(GetDogsDto dto)
    {
        var query = dogRepository.GetQueryable();

        query = dto.SortAttribute switch
        {
            DogSortAttributes.Name => dto.Desc ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name),
            DogSortAttributes.Color => dto.Desc ? query.OrderByDescending(d => d.Color) : query.OrderBy(d => d.Color),
            DogSortAttributes.TailLength => dto.Desc ? query.OrderByDescending(d => d.TailLenght) : query.OrderBy(d => d.TailLenght),
            DogSortAttributes.Weight => dto.Desc ? query.OrderByDescending(d => d.Weight) : query.OrderBy(d => d.Weight),
            _ => query.OrderBy(d => d.Name)
        };
        
        query = query.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize);
        return await Task.FromResult(query.ToList());
    }

    public async Task<Dog> CreateDogAsync(CreateDogDto dto)
    {
        if (await dogRepository.ExistAsync(dto.Name)) throw new DogAlreadyExistsException(dto.Name);
        
        var dog = new Dog(dto.Name, dto.Color, dto.TailLength, dto.Weight);
        await dogRepository.AddAsync(dog);
        if (!await dogRepository.SaveChanges()) throw new DogCreationFailedException("Conflict when creating dog.");

        return dog;
    }
}