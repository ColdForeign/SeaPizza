using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using SeaPizza.Application.Common.Exceptions;
using SeaPizza.Application.Common.Mailing;
using SeaPizza.Application.Identity.Users;
using SeaPizza.Domain.Common;
using SeaPizza.Shared.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Identity.Services;

internal partial class UserService
{
    public async Task<string> CreateAsync(CreateUserRequest request, string origin)
    {
        var user = new SeaPizzaUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.", result.GetErrors());
        }

        await _userManager.AddToRoleAsync(user, SeaPizzaRoles.Basic);

        var messages = new List<string> { string.Format("User {0} Registered.", user.UserName) };

        //await _events.PublishAsync(new SeaPizzaUserCreatedEvent(user.Id));

        return string.Join(Environment.NewLine, messages);
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException("User Not Found.");

        string currentImage = user.ImageUrl ?? string.Empty;
        if (request.Image != null || request.DeleteCurrentImage)
        {
            user.ImageUrl = await _fileStorage.UploadAsync<SeaPizzaUser>(request.Image, FileType.Image);
            if (request.DeleteCurrentImage && !string.IsNullOrEmpty(currentImage))
            {
                string root = Directory.GetCurrentDirectory();
                _fileStorage.Remove(Path.Combine(root, currentImage));
            }
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        string? phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        //await _events.PublishAsync(new SeaPizzaUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException("Update profile failed", result.GetErrors());
        }
    }
}
