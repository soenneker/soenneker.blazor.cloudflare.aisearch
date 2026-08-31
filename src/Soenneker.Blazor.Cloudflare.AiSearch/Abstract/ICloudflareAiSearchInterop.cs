using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

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

    /// <summary>
    /// Applies supported presentation options to a rendered Cloudflare AI Search bar.
    /// </summary>
    /// <param name="searchBar">The rendered Cloudflare AI Search bar element.</param>
    /// <param name="hideSubmitButton">Whether the search bar's submit button should be hidden.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the options have been applied.</returns>
    ValueTask ConfigureSearchBar(ElementReference searchBar, bool hideSubmitButton, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dismisses the results panel for a rendered Cloudflare AI Search bar.
    /// </summary>
    /// <param name="searchBar">The rendered Cloudflare AI Search bar element.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the results panel has been dismissed.</returns>
    ValueTask DismissSearchBar(ElementReference searchBar, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes browser event handlers and observers associated with a rendered Cloudflare AI Search bar.
    /// </summary>
    /// <param name="searchBar">The rendered Cloudflare AI Search bar element.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the search bar resources have been released.</returns>
    ValueTask DisposeSearchBar(ElementReference searchBar, CancellationToken cancellationToken = default);
}
