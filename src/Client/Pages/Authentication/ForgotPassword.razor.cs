using SeaPizza.Client.Components.Common;
using SeaPizza.Client.Infrastructure.ApiClient;
using SeaPizza.Client.Shared;
using Microsoft.AspNetCore.Components;

namespace SeaPizza.Client.Pages.Authentication;

public partial class ForgotPassword
{
    private readonly ForgotPasswordRequest _forgotPasswordRequest = new();
    private CustomValidation? _customValidation;
    private bool BusySubmitting { get; set; }

    [Inject]
    private IUsersClient UsersClient { get; set; } = default!;

    private async Task SubmitAsync()
    {
        BusySubmitting = true;

        await ApiHelper.ExecuteCallGuardedAsync(
            () => UsersClient.ForgotPasswordAsync(_forgotPasswordRequest),
            Snackbar,
            _customValidation);

        BusySubmitting = false;
    }
}
