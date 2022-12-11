using SeaPizza.Application.Identity.Tokens;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Identity.Services;

internal class TokenService : ITokenService
{
    public Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
    {
        throw new System.NotImplementedException();
    }
}
