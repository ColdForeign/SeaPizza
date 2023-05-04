using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Threading.Tasks;

namespace SeaPizza.Client.Infrastructure.Common;

public static class AccessTokenProviderExtensions
{
    public static async Task<string?> GetAccessTokenAsync(this IAccessTokenProvider tokenProvider) =>
        (await tokenProvider.RequestAccessToken())
            .TryGetToken(out var token)
                ? token.Value
                : null;
}
