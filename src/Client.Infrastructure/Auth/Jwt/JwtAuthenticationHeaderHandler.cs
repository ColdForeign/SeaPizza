﻿using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using SeaPizza.Client.Infrastructure.Common;

namespace SeaPizza.Client.Infrastructure.Auth.Jwt;

public class JwtAuthenticationHeaderHandler : DelegatingHandler
{
    private readonly IAccessTokenProviderAccessor _tokenProviderAccessor;
    private readonly NavigationManager _navigation;

    public JwtAuthenticationHeaderHandler(IAccessTokenProviderAccessor tokenProviderAccessor, NavigationManager navigation)
    {
        _tokenProviderAccessor = tokenProviderAccessor;
        _navigation = navigation;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // skip token endpoints
        if (request.RequestUri?.AbsolutePath.Contains("/tokens") is not true)
        {
            if (await _tokenProviderAccessor.TokenProvider.GetAccessTokenAsync() is string token)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _navigation.NavigateTo("/login");
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
