using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace SeaPizza.Infrastructure.Identity;

internal static class IdentityResultExtensions
{
    public static List<string> GetErrors(this IdentityResult result) =>
        result.Errors.Select(e => e.Description.ToString()).ToList();
}
