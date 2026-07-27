namespace Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

/// <summary>
/// Configuration for the Cloudflare AI Search modal.
/// </summary>
public sealed class CloudflareAiSearchModalConfiguration : CloudflareAiSearchSearchConfiguration
{
    /// <summary>
    /// The shortcut key, used with Command or Control.
    /// </summary>
    public string? Shortcut { get; set; }

    /// <summary>
    /// Controls whether the shortcut requires Command or Control.
    /// </summary>
    public bool? UseMetaKey { get; set; }
}
