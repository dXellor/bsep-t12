namespace bsep_bll.Dtos.Users;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}