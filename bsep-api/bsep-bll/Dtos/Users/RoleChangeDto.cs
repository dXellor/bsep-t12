using bsep_dll.Models.Enums;

namespace bsep_bll.Dtos.Users;

public class RoleChangeDto
{
    public string Email { get; set; }
    public UserRoleEnum NewRole { get; set; }
}