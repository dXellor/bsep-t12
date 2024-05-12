namespace bsep_bll.Dtos.Users;

public class UserRegistrationDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordAgain { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPib { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Type { get; set; }
    public string Package { get; set; }

    public UserRegistrationDto(string email, string password, string passwordAgain, string? firstName, string? lastName, string? companyName, string? companyPib, string address, string city, string country, string phone, string type, string package)
    {
        Email = email;
        Password = password;
        PasswordAgain = passwordAgain;
        FirstName = firstName;
        LastName = lastName;
        CompanyName = companyName;
        CompanyPib = companyPib;
        Address = address;
        City = city;
        Country = country;
        Phone = phone;
        Type = type;
        Package = package;
    }
}