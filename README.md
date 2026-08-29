[![](https://img.shields.io/nuget/v/soenneker.blazor.cloudflare.aisearch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.cloudflare.aisearch/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.cloudflare.aisearch/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.cloudflare.aisearch/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.cloudflare.aisearch.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.cloudflare.aisearch/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.cloudflare.aisearch)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.cloudflare.aisearch/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.cloudflare.aisearch/actions/workflows/codeql.yml)

# Soenneker.Blazor.Cloudflare.AiSearch

Blazor components for Cloudflare AI Search's search bar, search modal, chat bubble, and full-page chat snippets.

## Cloudflare setup

In the AI Search instance, enable its public endpoint and add the application's origin to **Authorized hosts**. Use that public endpoint in the component; do not put Cloudflare API tokens or other secrets in a Blazor application.

## Installation and registration

```bash
dotnet add package Soenneker.Blazor.Cloudflare.AiSearch
```

```csharp
using Soenneker.Blazor.Cloudflare.AiSearch.Registrars;

builder.Services.AddCloudflareAiSearchInteropAsScoped();
```

## Search bar

```razor
@using Soenneker.Blazor.Cloudflare.AiSearch
@using Soenneker.Blazor.Cloudflare.AiSearch.Configuration

<CloudflareAiSearchBar Configuration="_search" />

@code {
    private readonly CloudflareAiSearchBarConfiguration _search = new()
    {
        ApiUrl = "https://your-instance.search.ai.cloudflare.com/",
        Placeholder = "Search the documentation...",
        MaxResults = 50,
        MaxRenderResults = 10,
        DebounceMilliseconds = 250,
        ShowUrl = true,
        ShowDate = true
    };
}
```

The component loads Cloudflare's ES module after its first render. `ApiUrl` must be an absolute HTTPS endpoint without credentials, a query, or a fragment. Loopback HTTP is accepted for local development.

## Available components

| Component | Configuration | Use |
| --- | --- | --- |
| `CloudflareAiSearchBar` | `CloudflareAiSearchBarConfiguration` | Inline search input and results |
| `CloudflareAiSearchModal` | `CloudflareAiSearchModalConfiguration` | Search dialog, including keyboard shortcut options |
| `CloudflareAiSearchChatBubble` | `CloudflareAiSearchChatBubbleConfiguration` | Floating conversational search |
| `CloudflareAiSearchChatPage` | `CloudflareAiSearchChatPageConfiguration` | Full-page conversational search |

All configurations support `Theme`, `Placeholder`, `HideBranding`, and JSON `Translations`. Search configurations add result limits, debounce, URL/date display, grouping, and JSON `RequestOptions`. Chat configurations add JSON `ChatQueryRewrite`.

## Snippet version and hosting

By default, the module URL is built from the public endpoint and snippet version `0.0.25`:

```csharp
private readonly CloudflareAiSearchBarConfiguration _search = new()
{
    ApiUrl = "https://your-instance.search.ai.cloudflare.com/",
    ScriptVersion = "0.0.25"
};
```

Set `ScriptUrl` to an absolute module URL when self-hosting or pinning a different distribution:

```csharp
ScriptUrl = "https://static.example.com/cloudflare/search-snippet.es.js"
```

The module URL must use HTTPS, except for loopback HTTP development URLs. It is executable browser code, so only configure an origin you trust and allow it in the application's Content Security Policy. Search queries and chat messages are sent to the configured public endpoint; apply appropriate disclosure and consent for the indexed content and user input.
