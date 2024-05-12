using bsep_dll.Models.Enums;

namespace bsep_dll.Models;

public class User
{
    public virtual UserIdentity? UserIdentity { get; init; }
    
    public int Id { get; init; }
    public string Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? CompanyName { get; init; }
    public string? CompanyPib { get; init; }
    public string Address { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public string Phone { get; init; }
    public UserRoleEnum Role { get; set; }
    public UserTypeEnum Type { get; init; }
    public PackageTypeEnum Package { get; init; }
    
    public User(){}
    
    public User(int id, string email, string? firstName, string? lastName, string? companyName, string? companyPib, string address, string city, string country, string phone, UserRoleEnum role, UserTypeEnum type, PackageTypeEnum package)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CompanyName = companyName;
        CompanyPib = companyPib;
        Address = address;
        City = city;
        Country = country;
        Phone = phone;
        Role = role;
        Type = type;
        Package = package;
    }
}