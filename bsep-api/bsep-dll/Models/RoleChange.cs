using bsep_dll.Models.Enums;

namespace bsep_dll.Models;

public class RoleChange
{
    public string Email { get; init; }
    public UserRoleEnum NewRole { get; init; }
    
    public RoleChange(){}

    public RoleChange(string email, UserRoleEnum newRole)
    {
        Email = email;
        NewRole = newRole;
    }
}