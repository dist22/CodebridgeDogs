using CodebridgeDogs.Dto_s;
using CodebridgeDogs.Enums;
using CodebridgeDogs.Exceptions;
using CodebridgeDogs.Interfaces.IRepositories;
using CodebridgeDogs.Models;
using CodebridgeDogs.Services;
using Moq;
using Xunit;

namespace CodebridgeDogs.Tests;

public class DogServiceTests
{
    private readonly Mock<IDogRepository> _repoMock;
    private readonly DogService _service;

    public DogServiceTests()
    {
        _repoMock = new Mock<IDogRepository>();
        _service = new DogService(_repoMock.Object);
    }

    [Fact]
    public async Task CreateDogAsync_ShouldThrow_WhenDogExists()
    {
        _repoMock.Setup(r => r.ExistAsync("Neo")).ReturnsAsync(true);

        await Assert.ThrowsAsync<DogAlreadyExistsException>(() => 
            _service.CreateDogAsync(new CreateDogDto("Neo", "red", 22, 32)));
    }

    [Fact]
    public async Task CreateDogAsync_ShouldAddDog_WhenNotExists()
    {
        _repoMock.Setup(r => r.ExistAsync("Doggy")).ReturnsAsync(false);
        _repoMock.Setup(r => r.AddAsync(It.IsAny<Dog>())).Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChanges()).ReturnsAsync(true);

        var result = await _service.CreateDogAsync(new CreateDogDto("Doggy","red",10,20));

        Assert.Equal("Doggy", result.Name);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<Dog>()), Times.Once);
        _repoMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task GetDogsAsync_ShouldReturnSortedDogs_Desc()
    {
        var dogs = new List<Dog>
        {
            new Dog("Neo","red",22,32),
            new Dog("Jessy","black",7,14)
        }.AsQueryable();

        _repoMock.Setup(r => r.GetQueryable()).Returns(dogs);

        var dto = new GetDogsDto(DogSortAttributes.Weight, 1, 10, true);

        var result = await _service.GetDogsAsync(dto);

        Assert.Equal(32, result.First().Weight);
    }

    [Fact]
    public async Task GetDogsAsync_ShouldPaginateCorrectly()
    {
        var dogs = new List<Dog>();
        for(int i=1;i<=20;i++)
            dogs.Add(new Dog($"Dog{i}", "color", i, i*2));
        
        _repoMock.Setup(r => r.GetQueryable()).Returns(dogs.AsQueryable());

        var dto = new GetDogsDto(DogSortAttributes.Name, 1, 5, true); 
        var result = (await _service.GetDogsAsync(dto)).ToList();

        Assert.Equal("Dog9", result.First().Name);
        Assert.Equal(5, result.Count);
    }
}