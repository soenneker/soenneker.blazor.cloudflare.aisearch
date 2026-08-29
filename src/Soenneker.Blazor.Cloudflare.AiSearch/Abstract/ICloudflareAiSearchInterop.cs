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
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Cloudflare Ai Search is ready for use.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="scriptUrl"/> is not an absolute HTTPS URL or a loopback HTTP URL.</exception>
    ValueTask Initialize(string scriptUrl, CancellationToken cancellationToken = default);
}
