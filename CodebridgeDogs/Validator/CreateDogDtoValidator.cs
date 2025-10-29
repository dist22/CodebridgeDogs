using CodebridgeDogs.Dto_s;
using FluentValidation;

namespace CodebridgeDogs.Validator;

public class CreateDogDtoValidator : AbstractValidator<CreateDogDto>
{
    public CreateDogDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        
        RuleFor(x => x.Color)
            .NotEmpty().WithMessage("Color is required");
        
        RuleFor(x => x.TailLength)
            .NotEmpty().WithMessage("TailLenght is required")
            .GreaterThan(0).WithMessage("TailLenght must be greater than 0");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required")
            .GreaterThan(0).WithMessage("Weight must be greater than 0");

    }
}