using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SeaPizza.Shared.Authorization;

public static class SeaPizzaRoles
{
    public const string Admin = nameof(Admin);
    public const string Basic = nameof(Basic);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        Basic
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}
