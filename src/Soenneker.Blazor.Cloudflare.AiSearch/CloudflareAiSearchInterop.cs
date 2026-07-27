using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Atomics.ValueBools;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Blazor.Cloudflare.AiSearch.Abstract;

namespace Soenneker.Blazor.Cloudflare.AiSearch;

/// <inheritdoc cref="ICloudflareAiSearchInterop"/>
public sealed class CloudflareAiSearchInterop : ICloudflareAiSearchInterop
{
    private const string _modulePath = "_content/Soenneker.Blazor.Cloudflare.AiSearch/js/aisearchinterop.js";
    private const string _jsInitialize = "AiSearchInterop.initialize";

    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private ValueAtomicBool _disposed;

    public CloudflareAiSearchInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeModule);
    }

    private async ValueTask InitializeModule(CancellationToken cancellationToken)
    {
        await _resourceLoader.LoadModuleScript(_modulePath, cancellationToken: cancellationToken);
    }

    private async ValueTask EnsureInitialized(CancellationToken cancellationToken)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _initializer.Init(linked);
        }
    }

    public async ValueTask Initialize(string scriptUrl, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed.Value, this);

        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await EnsureInitialized(linked);
            await _jsRuntime.InvokeVoidAsync(_jsInitialize, linked, scriptUrl);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed.TrySetTrue())
            return;

        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
