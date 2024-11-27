using FluentValidation;
using Web_Lab2.Dtos.DogShelter;

namespace Web_Lab2.Validators
{
    public class DogShelterCreateDtoValidator : AbstractValidator<DogShelterCreateDto>
    {
        public DogShelterCreateDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Shelter name is required.")
            .Length(2, 100).WithMessage("Shelter name must be between 2 and 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Shelter address is required.")
                .Length(10, 200).WithMessage("Address must be between 10 and 200 characters.");

            RuleFor(x => x.ContactNumber)
                .Matches(@"^\+?\d{5,15}$").WithMessage("Invalid phone number format.");
        }
    }
}
