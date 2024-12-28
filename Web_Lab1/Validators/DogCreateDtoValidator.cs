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
                .NotEqual("string").WithMessage("Name cannot be equal «string»")
                .Length(2, 50).WithMessage("Dog name must be between 2 and 50 characters.");

            RuleFor(x => x.Breed)
                .NotEmpty().WithMessage("Dog name is required.")
                .NotEqual("string").WithMessage("Name cannot be equal «string»")
                .Length(2, 50).WithMessage("Dog name must be between 2 and 50 characters.");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 30).WithMessage("Dog age must be between 0 and 30 years.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Dog weight must be between 0 and 100 kg.");

            RuleFor(x => x.ShelterId)
                .GreaterThan(0).WithMessage("Shelter ID cannot be 0 or less. You can write «null» instead.");
        }
    }
}
