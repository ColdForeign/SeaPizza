using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SeaPizza.Client.Infrastructure.Auth.Jwt;

internal class AccessTokenProviderAccessor : IAccessTokenProviderAccessor
{
    private readonly IServiceProvider _provider;
    private IAccessTokenProvider? _tokenProvider;

    public AccessTokenProviderAccessor(IServiceProvider provider) =>
        _provider = provider;

    public IAccessTokenProvider TokenProvider =>
        _tokenProvider ??= _provider.GetRequiredService<IAccessTokenProvider>();
}
