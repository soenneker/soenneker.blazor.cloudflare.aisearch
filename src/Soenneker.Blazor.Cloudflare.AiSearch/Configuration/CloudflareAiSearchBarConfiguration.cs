namespace Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

/// <summary>
/// Configuration for the Cloudflare AI Search bar.
/// </summary>
public sealed class CloudflareAiSearchBarConfiguration : CloudflareAiSearchSearchConfiguration
{
    /// <summary>
    /// Hides the search bar's submit button, leaving the text input as the complete search control.
    /// </summary>
    public bool HideSubmitButton { get; set; }
}
