using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SeaPizza.Application.Common.Exceptions;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Identity.Services;

internal partial class UserService
{
    private async Task<string> GetEmailVerificationUriAsync(SeaPizzaUser user, string origin)
    {
        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        const string route = "api/users/confirm-email/";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), QueryStringKeys.UserId, user.Id);
        verificationUri = QueryHelpers.AddQueryString(verificationUri, QueryStringKeys.Code, code);
        verificationUri = QueryHelpers.AddQueryString(verificationUri, MultitenancyConstants.TenantIdName, _currentTenant.Id!);
        return verificationUri;
    }

    public async Task<string> ConfirmEmailAsync(string userId, string code, string tenant, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Where(u => u.Id == userId && !u.EmailConfirmed)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new InternalServerException("An error occurred while confirming E-Mail.");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        return result.Succeeded
            ? string.Format($"Account Confirmed for E-Mail {user.Email}. You can now use the /api/tokens endpoint to generate JWT.")
            : throw new InternalServerException(string.Format($"An error occurred while confirming {user.Email}"));
    }

    public async Task<string> ConfirmPhoneNumberAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new InternalServerException("An error occurred while confirming Mobile Phone.");
        if (string.IsNullOrEmpty(user.PhoneNumber)) throw new InternalServerException("An error occurred while confirming Mobile Phone.");

        var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);

        return result.Succeeded
            ? user.PhoneNumberConfirmed
                ? string.Format($"Account Confirmed for Phone Number {user.PhoneNumber}. You can now use the /api/tokens endpoint to generate JWT.")
                : string.Format($"Account Confirmed for Phone Number {user.PhoneNumber}. You should confirm your E-mail before using the /api/tokens endpoint to generate JWT.")
            : throw new InternalServerException(string.Format($"An error occurred while confirming {user.PhoneNumber}"));
    }
}
