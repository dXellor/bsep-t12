using bsep_dll.Helpers.Encryption;
using bsep_dll.Models.Enums;

namespace bsep_dll.Models;

public class User
{
    public virtual UserIdentity? UserIdentity { get; init; }
    
    public int Id { get; init; }
    public string Email { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyPib { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public UserRoleEnum Role { get; set; }
    public UserTypeEnum Type { get; init; }
    public PackageTypeEnum Package { get; init; }
    
    public User(){}

    public User(User objectToCopy)
    {
        Id = objectToCopy.Id;
        Email = objectToCopy.Email;
        FirstName = objectToCopy.FirstName;
        LastName = objectToCopy.LastName;
        CompanyName = objectToCopy.CompanyName;
        CompanyPib = objectToCopy.CompanyPib;
        Address = objectToCopy.Address;
        City = objectToCopy.City;
        Country = objectToCopy.Country;
        Phone = objectToCopy.Phone;
        Role = objectToCopy.Role;
        Type = objectToCopy.Type;
        Package = objectToCopy.Package;
    }
    
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

    public void EncryptData(string key, string iv)
    {
        FirstName = EncryptionUtils.EncryptWithEncodingType(FirstName, key, iv, EncodingType.UTF8);
        LastName = EncryptionUtils.EncryptWithEncodingType(LastName, key, iv, EncodingType.UTF8);
        Address = EncryptionUtils.EncryptWithEncodingType(Address, key, iv, EncodingType.UTF8);
        City = EncryptionUtils.EncryptWithEncodingType(City, key, iv, EncodingType.UTF8);
        Country = EncryptionUtils.EncryptWithEncodingType(Country, key, iv, EncodingType.UTF8);
        Phone = EncryptionUtils.EncryptWithEncodingType(Phone, key, iv, EncodingType.UTF8);
    }
    
    public void DecryptData(string key, string iv)
    {
        FirstName = EncryptionUtils.DecryptWithEncodingType(FirstName, key, iv, EncodingType.UTF8);
        LastName = EncryptionUtils.DecryptWithEncodingType(LastName, key, iv, EncodingType.UTF8);
        Address = EncryptionUtils.DecryptWithEncodingType(Address, key, iv, EncodingType.UTF8);
        City = EncryptionUtils.DecryptWithEncodingType(City, key, iv, EncodingType.UTF8);
        Country = EncryptionUtils.DecryptWithEncodingType(Country, key, iv, EncodingType.UTF8);
        Phone = EncryptionUtils.DecryptWithEncodingType(Phone, key, iv, EncodingType.UTF8);
    }

    public void UpdateAllowedValues(User updatedUser)
    {
        FirstName = updatedUser.FirstName;
        LastName = updatedUser.LastName;
        CompanyName = updatedUser.CompanyName;
        CompanyPib = updatedUser.CompanyPib;
        Address = updatedUser.Address;
        City = updatedUser.City;
        Country = updatedUser.Country;
        Phone = updatedUser.Phone;
        Role = updatedUser.Role;
    }
}