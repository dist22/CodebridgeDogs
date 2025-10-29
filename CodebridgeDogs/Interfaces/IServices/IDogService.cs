using CodebridgeDogs.Dto_s;
using CodebridgeDogs.Models;

namespace CodebridgeDogs.Interfaces.IServices;

public interface IDogService
{
    public Task<IEnumerable<Dog>> GetDogsAsync(GetDogsDto dto);
    
    public Task<Dog> CreateDogAsync(CreateDogDto dto);
    
}