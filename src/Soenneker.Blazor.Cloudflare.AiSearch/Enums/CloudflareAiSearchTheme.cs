using Soenneker.Gen.EnumValues;

namespace Soenneker.Blazor.Cloudflare.AiSearch.Enums;

/// <summary>
/// The color theme used by a Cloudflare AI Search snippet.
/// </summary>
[EnumValue<string>]
public sealed partial class CloudflareAiSearchTheme
{
    /// <summary>
    /// Follow the user's system color scheme.
    /// </summary>
    public static readonly CloudflareAiSearchTheme Auto = new("auto");

    /// <summary>
    /// Use the light color scheme.
    /// </summary>
    public static readonly CloudflareAiSearchTheme Light = new("light");

    /// <summary>
    /// Use the dark color scheme.
    /// </summary>
    public static readonly CloudflareAiSearchTheme Dark = new("dark");
}
