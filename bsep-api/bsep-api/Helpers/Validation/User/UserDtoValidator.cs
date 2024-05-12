using bsep_bll.Dtos.Users;
using FluentValidation;

namespace bsep_api.Helpers.Validation.User;

public class UserDtoValidator: AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(userDto => userDto.Email).NotEmpty().EmailAddress().Length(1, 255);
        RuleFor(userDto => userDto.FirstName).NotEmpty().Length(1, 25);
        RuleFor(userDto => userDto.LastName).NotEmpty().Length(1, 25);
        RuleFor(userDto => userDto.Role).NotEmpty()
            .Must(r => r.Equals("Employee") || r.Equals("Client") || r.Equals("Administrator"));
    }
}