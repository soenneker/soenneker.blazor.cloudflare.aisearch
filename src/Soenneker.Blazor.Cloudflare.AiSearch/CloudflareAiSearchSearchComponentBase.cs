using System.Collections.Generic;
using Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

namespace Soenneker.Blazor.Cloudflare.AiSearch;

/// <summary>
/// Provides common configuration for Cloudflare AI Search result components.
/// </summary>
public abstract class CloudflareAiSearchSearchComponentBase<TConfiguration> : CloudflareAiSearchComponentBase<TConfiguration>
    where TConfiguration : CloudflareAiSearchSearchConfiguration, new()
{
    protected override void AddComponentAttributes(IDictionary<string, object?> attributes)
    {
        AddAttribute(attributes, "max-results", Configuration.MaxResults);
        AddAttribute(attributes, "max-render-results", Configuration.MaxRenderResults);
        AddAttribute(attributes, "debounce-ms", Configuration.DebounceMilliseconds);
        AddAttribute(attributes, "group-by", Configuration.GroupBy);
        AddAttribute(attributes, "request-options", Configuration.RequestOptions);

        if (Configuration.ShowUrl)
            attributes["show-url"] = "true";

        if (Configuration.ShowDate)
            attributes["show-date"] = "true";
    }
}
