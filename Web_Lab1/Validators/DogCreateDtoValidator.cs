using FluentValidation;
using Web_Lab2.Dtos.Dog;

namespace Web_Lab2.Validators
{
    public class DogCreateDtoValidator : AbstractValidator<DogCreateDto>
    {
        public DogCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Dog name is required.")
                .NotEqual("string")
                .Length(2, 50).WithMessage("Dog name must be between 2 and 50 characters.");

            RuleFor(x => x.Breed)
                .NotEqual("string")
                .NotEmpty().WithMessage("Dog breed is required.");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 30).WithMessage("Dog age must be between 0 and 30 years.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Dog weight must be between 0 and 100 kg.");

            RuleFor(x => x.ShelterId)
                .NotEmpty().WithMessage("Shelter ID is required.");
        }
    }
}
