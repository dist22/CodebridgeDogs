using CodebridgeDogs.Dto_s;
using CodebridgeDogs.Validator;
using FluentValidation.TestHelper;
using Xunit;

namespace CodebridgeDogs.Tests;

public class CreateDogDtoValidatorTests
{
    private readonly CreateDogDtoValidator _validator = new CreateDogDtoValidator();

    [Fact]
    public void Should_HaveError_When_NameEmpty()
    {
        var dto = new CreateDogDto("", "red", 10, 20);
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.Name);
    }

    [Fact]
    public void Should_HaveError_When_TailLengthNegative()
    {
        var dto = new CreateDogDto("Doggy","red",-5,10);
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.TailLength);
    }

    [Fact]
    public void Should_NotHaveError_When_Valid()
    {
        var dto = new CreateDogDto("Doggy","red",10,20);
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}