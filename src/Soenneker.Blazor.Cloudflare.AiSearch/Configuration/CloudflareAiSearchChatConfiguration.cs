namespace Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

/// <summary>
/// Configuration shared by Cloudflare AI Search chat components.
/// </summary>
public abstract class CloudflareAiSearchChatConfiguration : CloudflareAiSearchConfiguration
{
    /// <summary>
    /// JSON query rewrite options.
    /// </summary>
    public string? ChatQueryRewrite { get; set; }
}
