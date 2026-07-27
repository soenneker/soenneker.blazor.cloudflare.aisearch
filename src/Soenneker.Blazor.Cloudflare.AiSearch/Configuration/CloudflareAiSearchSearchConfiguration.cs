namespace Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

/// <summary>
/// Configuration shared by Cloudflare AI Search bar and modal components.
/// </summary>
public abstract class CloudflareAiSearchSearchConfiguration : CloudflareAiSearchConfiguration
{
    /// <summary>
    /// The maximum number of results requested.
    /// </summary>
    public int? MaxResults { get; set; }

    /// <summary>
    /// The maximum number of results rendered.
    /// </summary>
    public int? MaxRenderResults { get; set; }

    /// <summary>
    /// The input debounce delay, in milliseconds.
    /// </summary>
    public int? DebounceMilliseconds { get; set; }

    /// <summary>
    /// Shows result URLs.
    /// </summary>
    public bool ShowUrl { get; set; }

    /// <summary>
    /// Shows result dates.
    /// </summary>
    public bool ShowDate { get; set; }

    /// <summary>
    /// The metadata field used to group results.
    /// </summary>
    public string? GroupBy { get; set; }

    /// <summary>
    /// JSON request enrichment options.
    /// </summary>
    public string? RequestOptions { get; set; }
}
