using Microsoft.AspNetCore.Identity;
using System;

namespace SeaPizza.Infrastructure.Identity;

public class SeaPizzaRoleClaim : IdentityRoleClaim<string>
{
    public string? CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
}
