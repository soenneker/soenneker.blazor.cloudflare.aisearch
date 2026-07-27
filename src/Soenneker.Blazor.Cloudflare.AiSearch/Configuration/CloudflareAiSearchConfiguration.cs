using Soenneker.Blazor.Cloudflare.AiSearch.Enums;

namespace Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

/// <summary>
/// Common configuration for a Cloudflare AI Search component.
/// </summary>
public class CloudflareAiSearchConfiguration
{
    /// <summary>
    /// The public endpoint URL from the AI Search instance's Settings &gt; Public Endpoint page.
    /// </summary>
    public string ApiUrl { get; set; } = "";

    /// <summary>
    /// The input placeholder.
    /// </summary>
    public string? Placeholder { get; set; }

    /// <summary>
    /// The snippet color theme.
    /// </summary>
    public CloudflareAiSearchTheme Theme { get; set; } = CloudflareAiSearchTheme.Auto;

    /// <summary>
    /// Hides the Cloudflare branding when enabled.
    /// </summary>
    public bool HideBranding { get; set; }

    /// <summary>
    /// JSON translations that override the snippet's user-facing strings.
    /// </summary>
    public string? Translations { get; set; }

    /// <summary>
    /// Overrides the complete snippet ES module URL.
    /// </summary>
    public string? ScriptUrl { get; set; }

    /// <summary>
    /// The snippet asset version used when <see cref="ScriptUrl"/> is not specified.
    /// </summary>
    public string ScriptVersion { get; set; } = "0.0.25";
}
