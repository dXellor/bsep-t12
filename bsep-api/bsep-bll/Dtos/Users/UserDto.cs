using bsep_dll.Helpers.Pagination;
using bsep_dll.Models;

namespace bsep_bll.Dtos.Users;

public class UserDto
{
    public int Id { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    
    public UserDto(int id, string firstName, string lastName, string email, string role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Role = role;
    }
}