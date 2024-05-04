using bsep_bll.Dtos.Users;
using FluentValidation;

namespace bsep_api.Helpers.Validation.Auth;

public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.Email).NotEmpty().EmailAddress();
        RuleFor(loginDto => loginDto.Password).NotEmpty();
    }
}