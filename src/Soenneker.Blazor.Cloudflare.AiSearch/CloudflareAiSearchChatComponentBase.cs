using System.Collections.Generic;
using Soenneker.Blazor.Cloudflare.AiSearch.Configuration;

namespace Soenneker.Blazor.Cloudflare.AiSearch;

/// <summary>
/// Provides common configuration for Cloudflare AI Search chat components.
/// </summary>
public abstract class CloudflareAiSearchChatComponentBase<TConfiguration> : CloudflareAiSearchComponentBase<TConfiguration>
    where TConfiguration : CloudflareAiSearchChatConfiguration, new()
{
    protected override void AddComponentAttributes(IDictionary<string, object?> attributes)
    {
        AddAttribute(attributes, "chat-query-rewrite", Configuration.ChatQueryRewrite);
    }
}
