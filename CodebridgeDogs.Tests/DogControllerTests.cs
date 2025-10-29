using CodebridgeDogs.Controllers;
using CodebridgeDogs.Dto_s;
using CodebridgeDogs.Enums;
using CodebridgeDogs.Interfaces.IServices;
using CodebridgeDogs.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CodebridgeDogs.Tests;

public class DogsControllerTests
{
    private readonly Mock<IDogService> _serviceMock;
    private readonly DogsController _controller;

    public DogsControllerTests()
    {
        _serviceMock = new Mock<IDogService>();
        _controller = new DogsController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetPing_ShouldReturnCorrectString()
    {
        var result = _controller.Get() as OkObjectResult;
        Assert.Equal("Dogshouseservice.Version1.0.1", result.Value);
    }

    [Fact]
    public async Task GetDogs_ShouldReturnOkWithData()
    {
        _serviceMock.Setup(s => s.GetDogsAsync(It.IsAny<GetDogsDto>()))
            .ReturnsAsync(new List<Dog>{ new Dog("Neo","red",22,32) });

        var result = await _controller.GetDogs(new GetDogsDto(DogSortAttributes.Name)) as OkObjectResult;

        var list = Assert.IsAssignableFrom<IEnumerable<Dog>>(result.Value);
        Assert.Single(list);
    }
    

    [Fact]
    public async Task PostDog_ShouldReturnOk_WhenDogCreated()
    {
        _serviceMock.Setup(s => s.CreateDogAsync(It.IsAny<CreateDogDto>()))
            .ReturnsAsync(new Dog("Doggy","red",10,20));

        var result = await _controller.PostDog(new CreateDogDto("Doggy","red",10,20)) as OkObjectResult;

        Assert.Equal("Doggy", ((Dog)result.Value).Name);
    }
}