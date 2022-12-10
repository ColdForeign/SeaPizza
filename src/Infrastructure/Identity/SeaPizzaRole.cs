using Microsoft.AspNetCore.Identity;

namespace SeaPizza.Infrastructure.Identity;

public class SeaPizzaRole : IdentityRole
{
    public string? Description { get; set; }

    public SeaPizzaRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }

}
