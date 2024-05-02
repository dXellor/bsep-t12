using bsep_dll.Models.Enums;

namespace bsep_dll.Models;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public UserRoleEnum Role { get; init; }
    
    public User(int id, string firstName, string lastName, string email, UserRoleEnum role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
    }
}