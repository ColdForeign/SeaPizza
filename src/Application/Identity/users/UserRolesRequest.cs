using System.Collections.Generic;

namespace SeaPizza.Application.Identity.Users;

public class UserRolesRequest
{
    public List<UserRoleDto> UserRoles { get; set; } = new();
}
