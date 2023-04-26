using SeaPizza.Application.Common.Models;

namespace SeaPizza.Application.Identity.Users;

public class UserListFilter : PaginationFilter
{
    public bool? IsActive { get; set; }
}
