using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Cloudflare.AiSearch.Abstract;

/// <summary>
/// Blazor interop for browser-facing functionality exposed by this package.
/// </summary>
public interface ICloudflareAiSearchInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the package interop and the Cloudflare AI Search snippet module have been loaded.
    /// </summary>
    /// <param name="scriptUrl">The absolute URL of Cloudflare's AI Search snippet ES module.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    ValueTask Initialize(string scriptUrl, CancellationToken cancellationToken = default);
}
