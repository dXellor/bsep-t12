using bsep_bll.Dtos.Users;
using FluentValidation;

namespace bsep_api.Helpers.Validation.User;

public class UserRegistrationDtoValidator: AbstractValidator<UserRegistrationDto>
{
    public UserRegistrationDtoValidator()
    {
        RuleFor(registrationDto => registrationDto.Email).NotEmpty().EmailAddress().Length(1, 255);
        RuleFor(registrationDto => registrationDto.Address).NotEmpty().Length(1, 255);
        RuleFor(registrationDto => registrationDto.City).NotEmpty().Length(1, 255);
        RuleFor(registrationDto => registrationDto.Country).NotEmpty().Length(1, 255);
        RuleFor(registrationDto => registrationDto.Phone).NotEmpty().Length(10, 20).Matches(@"[+]?[\ 0-9]{10,20}");
        RuleFor(registrationDto => registrationDto.Type).NotEmpty().Must(t => t.Equals("PhysicalEntity") ||  t.Equals("LegalEntity"));
        RuleFor(registrationDto => registrationDto.Package).NotEmpty().Must(t => t.Equals("Basic") ||  t.Equals("Standard") || t.Equals("Gold"));
        
        RuleFor(registrationDto => registrationDto.FirstName).NotEmpty().Length(1, 25).When(registrationDto => registrationDto.Type.Equals("PhysicalEntity"));
        RuleFor(registrationDto => registrationDto.LastName).NotEmpty().Length(1, 25).When(registrationDto => registrationDto.Type.Equals("PhysicalEntity"));
        
        RuleFor(registrationDto => registrationDto.CompanyName).NotEmpty().Length(1, 50).When(registrationDto => registrationDto.Type.Equals("LegalEntity"));
        RuleFor(registrationDto => registrationDto.CompanyPib).NotEmpty().Length(9).When(registrationDto => registrationDto.Type.Equals("LegalEntity"));
        
        //Password rules
        RuleFor(registrationDto => registrationDto)
            .Must(registrationDto => registrationDto.Password.Equals(registrationDto.PasswordAgain)).WithMessage("Passwords do not match");
        
        RuleFor(registrationDto => registrationDto.Password)
            .MinimumLength(16).WithMessage("Your password must be at least 16 characters long")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.\$\-_]+").WithMessage("Your password must contain at least one special character.");
    }
}