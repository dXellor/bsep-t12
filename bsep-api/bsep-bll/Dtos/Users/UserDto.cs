using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;

namespace bsep_bll.Dtos.Users;

public class UserDto
{
    public int Id { get; init; }
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPib { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
    public string Type { get; set; }
    public string Package { get; set; }
    
    public UserDto(){}
    
    public UserDto(int id, string email, string? firstName, string? lastName, string? companyName, string? companyPib, string address, string city, string country, string phone, string role, string type, string package)
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